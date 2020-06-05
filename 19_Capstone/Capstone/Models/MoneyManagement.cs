using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class MoneyManagement
    {
        public decimal CurrentMoney { get; private set; }

        private VendoMatic800 Machine;
        public MoneyManagement(VendoMatic800 machine)
        {
            Machine = machine;
            CurrentMoney = 0;
        }



        private Dictionary<string, decimal> Amounts = new Dictionary<string, decimal>()
        {
            {"n", .05M },
            {"d", .1M },
            {"q", .25M },
            {"1", 1 },
            {"2", 2 },
            {"5", 5 },
            {"10", 10 }
        };


        public bool Purchase(decimal cost)
        {
            if (CurrentMoney >= cost)
            {
                decimal startingMoney = CurrentMoney;
                CurrentMoney = CurrentMoney - cost;

                

                return true;
            }
            else
            {
                return false;
            }

        }

        public void FeedMoney(decimal amount)
        {
            decimal startingMoney = CurrentMoney;
            CurrentMoney = CurrentMoney + amount;
            Machine.AuditWriter("fm", startingMoney);
        }

        public string GiveChange()
        {
            int qCounter = 0;
            int dCounter = 0;
            int nCounter = 0;

            decimal change = CurrentMoney;

            while (change >= .25M)
            {
                change = change - .25M;
                qCounter++;
            }

            while (change >= .1M)
            {
                change = change - .1M;
                dCounter++;
            }

            while (change >= .05M)
            {
                change = change - .05M;
                nCounter++;
            }
            decimal startingMoney = CurrentMoney;
            CurrentMoney = change;
            Machine.AuditWriter("gc", startingMoney);
            return $"Your change back is {qCounter} quarters, {dCounter} dimes, and {nCounter} nickels.";



        }
    }
}
