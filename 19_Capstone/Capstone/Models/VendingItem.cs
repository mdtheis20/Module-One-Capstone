using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class VendingItem
    {
        public string SlotLocation { get; }

        public string ProductName { get; }

        public string Type { get; }

        public decimal Price { get; }
        public int Count { get; set; }

        public VendingItem(string slot, string name, decimal price, string type, int count)
            {
            this.SlotLocation = slot;
            this.ProductName = name;
            this.Price = price;
            this.Type = type;
            this.Count = count;

            }



        public override string ToString()
        {
            if (Type == "Chip")
            {
                return "Crunch Crunch, Yum!";
            }

            if (Type == "Candy")
            {
                return "Munch Munch, Yum!";
            }

            if (Type == "Drink")
            {
                return "Glug Glug, Yum!";
            }

            if (Type == "Gum")
            {
                return "Chew Chew, Yum!";
            }

            return "";
        }

    }
}
