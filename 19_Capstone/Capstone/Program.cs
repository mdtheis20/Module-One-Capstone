using System;
using System.Collections.Generic;
using Capstone.Models;
using CLI;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendoMatic800 machine = new VendoMatic800();

            
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Go Browns!!"));
            
            Console.WriteLine("Go Browns!");
            MainMenu main = new MainMenu(machine);
            main.Run();

            
        }
    }
}
