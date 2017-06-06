﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E2Print.WebUI.Models
{
    public class ProductViewModel
    {
        private string _name = ""; //default value
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

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}