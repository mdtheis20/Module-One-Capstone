using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Logs
    {
        private string aPath { get; }

        private string sPath { get; }

        public Logs()
        {
            aPath = "..\\..\\..\\LogsLocations\\Log.txt";

            sPath = $"..\\..\\..\\LogsLocations\\SalesReport_{DateTime.Now.ToString("'yyyy'-'mm'-'dd'T'HH''mm'")}";
        }

    }
}
