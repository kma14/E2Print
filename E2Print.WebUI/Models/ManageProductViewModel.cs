using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E2Print.Domain.Entities;

namespace E2Print.WebUI.Models
{
    public class ManageProductViewModel
    {
        public List<Category> CategoryList { get; set; }
        public List<Product> ProductList { get; set; }

    }
}