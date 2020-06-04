using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class LogsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> testList = new List<string>()
            {
                $"{DateTime.Now.ToString()} FEED MONEY: $5.00 $5.00",
                $"{DateTime.Now.ToString()} FEED MONEY: $5.00 $10.00",
                $"{DateTime.Now.ToString()} Crunchie B4 $10.00 $8.50",
            };
            
           
        }
    }
}
