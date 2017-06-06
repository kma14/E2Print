using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E2Print.WebUI.Models
{
    public class EmailViewModel
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool EnableSsl { get; set; }
    }
}