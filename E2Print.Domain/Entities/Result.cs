using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2Print.Domain.Entities
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}
