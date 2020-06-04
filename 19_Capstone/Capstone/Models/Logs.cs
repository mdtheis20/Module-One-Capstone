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
            sPath = $"..\\..\\..\\LogsLocations\\SalesReport";

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

        public void EndOfDay(List<string> salesLog, List<string> auditLog)
        {
            using (StreamWriter writer = new StreamWriter(aPath, true))
            {
                foreach(string line in auditLog)
                {
                    writer.WriteLine();
                }
            }
            
            using (StreamWriter salesWriter = new StreamWriter(sPath, false))
            {
                foreach(string line in salesLog)
                {
                    salesWriter.WriteLine();
                }
            }
        }
    }
}
