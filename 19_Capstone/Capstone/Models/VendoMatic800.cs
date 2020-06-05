using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    public class VendoMatic800
    {
        private List<string> salesLog;

        private List<string> auditLog;

        private MoneyManagement manager;
        private Logs logs;

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
            decimal priceOfProduct = ProductLeft[slotNumber].Price;
            bool isEnoughMoney = ( manager.CurrentMoney >= priceOfProduct);
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
        
    }
}
