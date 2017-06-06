using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2Print.WebUI.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult LookAndFeel()
        {
            return View();
        }
    }
}