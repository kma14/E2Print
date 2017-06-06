using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2Print.Domain.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public decimal Discount { get; set; }
    }
}
