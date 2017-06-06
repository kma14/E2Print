using E2Print.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E2Print.WebUI.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Promotion> Promotions { get; set; }
    }
}