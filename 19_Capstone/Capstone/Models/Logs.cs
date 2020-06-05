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

        public Logs()
        {
            aPath = "..\\..\\..\\LogsLocations\\Log.txt";
            //TODO: format file name for log
            sPath = $"..\\..\\..\\LogsLocations\\SalesReport.txt";
            itemPath = "..\\..\\..\\..\\vendingmachine.csv";
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