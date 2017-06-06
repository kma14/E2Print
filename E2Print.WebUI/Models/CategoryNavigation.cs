using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E2Print.Domain.Entities;

namespace E2Print.WebUI.Models
{
    public class CategoryNavigation
    {
        public List<Category> SubCategories{ get; set; }
        public Category Category { get; set; }
    }
}