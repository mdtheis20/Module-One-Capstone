using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CLI;

namespace Capstone.Models
{
    public class VendoMatic800
    {
        private List<string> salesLog;

        private List<string> auditLog;

        public MoneyManagement manager;
        private Logs logs;

        //public MainMenu MainMenu = new MainMenu();
        //public SubMenu1 PurchaseMenu = new SubMenu1("PurchaseMenu");
        //public SubMenu1 SelectProduct = new SubMenu1("SelectProduct");
        //public SubMenu1 FeedMoney = new SubMenu1("FeedMoney");
        //public SubMenu1 DisplayItems = new SubMenu1("DisplayItems");
        //public SubMenu1 DisplayMessage = new SubMenu1("DisplayMessage");








        public Dictionary<string, VendingItem> ProductLeft { get; private set; }

        public VendoMatic800()
        {
            logs = new Logs();
            manager = new MoneyManagement();
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
                ProductLeft[slotNumber].Count--;
                manager.Purchase(priceOfProduct);
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
                int displayNumber = line.Value.Count;
                Console.WriteLine($"{ line.Key} {displayName} {displayPrice} {displayNumber} left");
            }
        }

        public void EndOfDay()
        {
            logs.EndOfDay(salesLog, auditLog);
        }



    }
}

    




        

    

