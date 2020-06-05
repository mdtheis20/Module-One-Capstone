using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class LogsTest
    {        
        [TestMethod]
        public void TestLinks()
        {
            List<string> testList = new List<string>()
            {
                $"{DateTime.Now.ToString()} FEED MONEY: $5.00 $5.00",
                $"{DateTime.Now.ToString()} FEED MONEY: $5.00 $10.00",
                $"{DateTime.Now.ToString()} Crunchie B4 $10.00 $8.50",
                
            };

            List<string> salesLog = new List<string>()
            {
                $"Candy Bars | 5",
                $"Browns Jerseys | 2",
                $"Crunchie | 12",

            };
            Logs logs = new Logs();
            logs.EndOfDay(testList);
           
        }
    }
}
