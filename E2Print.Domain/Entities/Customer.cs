using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace E2Print.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Invalid Login, please enter only alphabets and numbers")]
        public string Login { get; set; }
        public string Gender { get; set; }

        [Required]
        [StringLength(15)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Invalid Login, please enter only alphabets and numbers")]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Invalid First name, please enter only alphabets and numbers")]
        public string FirstName { get;set;}
        [Required]
        [StringLength(20)]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Invalid Last name, please enter only alphabets and numbers")]
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        [Required]
        [RegularExpression( @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        public string Company { get; set; }

        public string Role { get; set; }
    }
}
