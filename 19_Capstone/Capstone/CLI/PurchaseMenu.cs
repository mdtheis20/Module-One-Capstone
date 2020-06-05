using Capstone.Models;
using System;
using System.Collections.Generic;

namespace CLI
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class PurchaseMenu : CLIMenu
    {
        // Store any private variables here....
        private VendoMatic800 Machine;        
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public PurchaseMenu(VendoMatic800 machine) :
            base("Purchase Menu")
        {
            // Store any values passed in....
            Machine = machine;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "Feed Money");
            this.menuOptions.Add("2", "Select Product");
            this.menuOptions.Add("3", "Finish Transaction");
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
                    FeedMoneyMenu feedMoney = new FeedMoneyMenu(Machine);
                    feedMoney.Run();                    
                    
                    Pause("");
                    return true;
                case "2": // Do whatever option 2 is
                    Machine.Display();
                    Console.Write("Enter slot location: ");
                    string slotLocation = Console.ReadLine().ToUpper();
                    string message = Machine.Purchase(slotLocation);
                    Console.WriteLine(message);
                  
                    Pause("");
                    return true;
                case "3":
                    string change = Machine.manager.GiveChange();
                    Console.WriteLine(change);
                    Pause("");
                    MainMenu main = new MainMenu(Machine);
                    main.Run();
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
