using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CLI;

namespace Capstone.Models
{
    public class VendoMatic800
    {        
        private List<string> auditLog = new List<string>();
        public MoneyManagement manager;
        public Logs logs;

        private Dictionary<string, string> auditLogWriter = new Dictionary<string, string>()
        {
            {"fm", "FEED MONEY" }, {"gc", "GIVE CHANGE"}
        };

        public Dictionary<string, VendingItem> ProductLeft { get; private set; }

        public VendoMatic800()
        {
            logs = new Logs(this);
            manager = new MoneyManagement(this);
            ProductLeft = logs.Load();


        }

        public string Purchase(string slotNumber)
        {
            //CLI menu takes care to check if slot is valid           
            // is there is enough money available
            bool isValidCode = ProductLeft.ContainsKey(slotNumber);
            if (!isValidCode)
            {
                return "Invalid Selection";
            }

            decimal priceOfProduct = ProductLeft[slotNumber].Price;
            bool isEnoughMoney = (manager.CurrentMoney >= priceOfProduct);
            bool isThereEnoughItems = ProductLeft[slotNumber].Count > 0;

            if (isEnoughMoney && isThereEnoughItems)
            {
                decimal startingMoney = manager.CurrentMoney;
                ProductLeft[slotNumber].Count--;
                manager.Purchase(priceOfProduct);
                this.AuditWriter(slotNumber, startingMoney);
                return ProductLeft[slotNumber].ToString();
            }
            else if (!isThereEnoughItems)
            {
                return "SOLD OUT";
            }
            else
            {
                return "Please add more money to make purchase.";
                //going back to purchase menu is taken care of by CLI
            }
        }

        public void Display()
        {
            foreach (var line in this.ProductLeft)
            {
                string displayName = line.Value.ProductName;
                string displayPrice = line.Value.Price.ToString("c");
                string spacer = "";
                for (int i = 1; i < (20 - displayName.Length); i++) // spacer to line up |
                {
                    spacer += " ";
                }
                int displayNumber = line.Value.Count;
                Console.WriteLine($"\t{ line.Key} | {displayName}{spacer} | {displayPrice} | {displayNumber} left");
            }
        }

        public void EndOfDay()
        {
            logs.EndOfDay(auditLog);
        }

        public void AuditWriter(string key, decimal startingMoney)
        {
            string time = DateTime.Now.ToString();
            string money = $"{startingMoney.ToString("c")} {manager.CurrentMoney.ToString("c")}";
           string transactionType = "";

            if (auditLogWriter.ContainsKey(key))
            {
                transactionType = auditLogWriter[key];
            }
            else if (ProductLeft.ContainsKey(key))
            {
                transactionType = ProductLeft[key].ProductName + " " +  "|" +ProductLeft[key].SlotLocation;
            }
            string entry = time + " " + transactionType + " " + money;

            auditLog.Add(entry);

        }



    }
}

    




        

    

