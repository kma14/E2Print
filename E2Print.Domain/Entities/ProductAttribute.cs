using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace E2Print.Domain.Entities
{
    public  class ProductAttribute
    {
        public int Id { get; set; }

        [Required]
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public string Description { get; set; }
    }
}
