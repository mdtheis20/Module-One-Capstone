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

       public Dictionary<string, List<VendingItem>> ProductLeft { get; }

        public VendoMatic800()
        {
            Logs logs = new Logs(); 
            
            
            
            ProductLeft = new Dictionary<string, List<VendingItem>>();
            ProductLeft = logs.Load();

        }


        
    }
}
