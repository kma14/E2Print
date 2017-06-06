using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E2Print.WebUI.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}