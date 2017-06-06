using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2Print.Domain.Entities
{
    public class CustomerFeedback
    {
        private DateTime? _commentDate = System.DateTime.Today;  //defale value
        private string _photo= null;  //defale value
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CommentDate
        {
            get {return this._commentDate;}
            set { this._commentDate = value; }
        }
        public string CustomerName { get; set; }

        public string Photo
        {
            get { return this._photo; }
            set { this._photo = value; }
        }
    }
}
