using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E2Print.WebUI.Models
{
    //http://stackoverflow.com/questions/271347/making-a-generic-property
    public class SelectListViewModel<T>
    {
        public List<string> Keys { get; set; }

        public List<T> Items { get; set; }
    }
}