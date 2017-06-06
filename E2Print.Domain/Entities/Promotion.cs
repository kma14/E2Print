using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2Print.Domain.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> PromotionPrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
    }
}
