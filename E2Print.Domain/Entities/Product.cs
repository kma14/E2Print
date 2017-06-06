using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2Print.Domain.Entities
{
    public class Product
    {
        private string _name=""; //default value
        private string _description = "";//default value

        public int Id { get; set; }
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public int BuyingQty { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}
