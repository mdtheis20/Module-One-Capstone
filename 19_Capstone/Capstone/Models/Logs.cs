using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    public class Logs
    {
        private string aPath { get; }        
        private string sPath { get; }
        private string itemPath { get; }
        private Dictionary<string, int> salesLog = new Dictionary<string, int>();
        private string salesLogPathName
        {
            get
            {
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = DateTime.Now.Day.ToString();
                string hours = DateTime.Now.Hour.ToString();
                string minutes = DateTime.Now.Minute.ToString();

                return year + month + day + hours + minutes;
            }
        }
        public VendoMatic800 Machine;


        public Logs(VendoMatic800 machine)
        {
            aPath = "..\\..\\..\\LogsLocations\\Log.txt";
            //TODO: format file name for log
            sPath = $"..\\..\\..\\LogsLocations\\SalesReport{salesLogPathName}.txt";
            itemPath = "..\\..\\..\\..\\vendingmachine.csv";
            SalesLogConstructor();
            Machine = machine;
        }

        public Dictionary<string, VendingItem> Load()
        {
            Dictionary<string, VendingItem> list = new Dictionary<string, VendingItem>();
            if (!File.Exists(itemPath))
            {
                return list;
            }
            using (StreamReader rdr = new StreamReader(itemPath))
            {
                while (!rdr.EndOfStream)
                {
                    string line = rdr.ReadLine();
                    string[] fields = line.Split("|");
                    decimal cost = decimal.Parse(fields[2]);
                    VendingItem item = new VendingItem(fields[0], fields[1], cost, fields[3], 5);
                    list.Add(item.SlotLocation, item);
                } 
            }
            return list;
        }

        public bool EndOfDay(List<string> auditLog)
        {
            
            try
            {
                using (StreamWriter writer = new StreamWriter(aPath, true))
                {
                    foreach (string line in auditLog)
                    {
                        writer.WriteLine(line);
                    }
                }


            }
            catch
            {
                return false;
            }
            
            return true;
        }

        public void PrintSalesLog()
        {
            try
            {
                using (StreamReader reader = new StreamReader(aPath))
                {

                    // reads through audit log and counts how much of each item was purchsed
                    // the count is stored in salesLog then printed to file
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.Contains("|"))
                        {
                            int index = line.IndexOf("|");
                            string slotLocation = line.Substring(index + 1, 2);
                            if (salesLog.ContainsKey(slotLocation))
                            {
                                salesLog[slotLocation]++;
                            }
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(sPath))
                {
                    string productName = "";
                    foreach (KeyValuePair<string, int> slot in salesLog)
                    {
                        if (Machine.ProductLeft.ContainsKey(slot.Key))
                        {
                            productName = Machine.ProductLeft[slot.Key].ProductName;
                        }

                        writer.WriteLine(productName + " | " + slot.Value);
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SalesLogConstructor()
        {
            //this method sets up a dictionary for the sales log
            //it can be changed to include more or less slot numbers            
            for (int i = 1; i <= 16; i++)
            {
                if (i <= 4)
                {
                    salesLog.Add($"A{i}", 0);
                }
                else if (i <= 8)
                {
                    salesLog.Add($"B{i - 4}", 0);
                }
                else if (i <= 12)
                {
                    salesLog.Add($"C{i - 8}", 0);
                }
                else
                {
                    salesLog.Add($"D{i - 12}", 0);
                }
            }
        }        
    }
}

//DateTime to the second
//what the action was (feed money or purchase)
//The current money before the transaction
//the current money after the transaction
//> 01/01/2016 12:00:00 PM FEED MONEY: $5.00 $5.00
//         >01/01/2016 12:00:15 PM FEED MONEY: $5.00 $10.00
//         >01/01/2016 12:00:20 PM Crunchie B4 $10.00 $8.50
//         >01/01/2016 12:01:25 PM Cowtales B2 $8.50 $7.50
//         >01/01/2016 12:01:35 PM GIVE CHANGE: $7.50 $0.00