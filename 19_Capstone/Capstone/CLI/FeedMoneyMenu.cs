using Capstone.Models;
using System;
using System.Collections.Generic;

namespace CLI
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class FeedMoneyMenu : CLIMenu
    {
        // Store any private variables here....
        private VendoMatic800 Machine;        
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public FeedMoneyMenu(VendoMatic800 machine) :
            base("Feed Money")
        {
            // Store any values passed in....
            Machine = machine;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "$1");
            this.menuOptions.Add("2", "$2");
            this.menuOptions.Add("5", "$5");
            this.menuOptions.Add("10", "$10");
            this.menuOptions.Add("B", "Purchase Menu");
            //this.quitKey = "B";
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1": // Do whatever option 1 is                    
                    Machine.manager.FeedMoney(1M);
                    Pause("");
                    return true;
                case "2":                   
                    Machine.manager.FeedMoney(2M);
                    Pause("");
                    return true;
                case "5":
                    Machine.manager.FeedMoney(5M);
                    Pause("");
                    return true;
                case "10":
                    Machine.manager.FeedMoney(10M);
                    Pause("");
                    return true;
                case "B": // Do whatever option 2 is
                    PurchaseMenu purchaseMenu = new PurchaseMenu(Machine);
                    purchaseMenu.Run();
                    
                    Pause("");
                    return false;
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Cyan);
            Console.WriteLine($"Current Money Provided: {Machine.manager.CurrentMoney.ToString("c")}");
            ResetColor();
        }

        private void PrintHeader()
        {
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Sub-Menu 1"));
            ResetColor();
        }

    }
}
