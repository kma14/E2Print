using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E2Print.Domain.Entities;

namespace E2Print.WebUI.Models
{
    public class CategoryAndProductsViewModel
    {
        public Category Category { get; set; }
        public bool OnSale { get; set; }
        public decimal Discount { get; set; }
        public List<Product> Products { get; set; }
        public List<string> Photos { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Colors { get; set; }
        public List<string> Materials { get; set; }
        public List<string> Packs { get; set; }
        public List<string> Frames { get; set; }
    }
}