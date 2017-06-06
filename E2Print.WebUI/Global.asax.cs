using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using E2Print.BL.Implements.EF;

namespace E2Print.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ICustomer repository;
        public MvcApplication()
        {
            //http://stackoverflow.com/questions/7752023/how-to-inject-dependencies-into-the-global-asax-cs
            this.repository = new CustomerBL();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //http://stackoverflow.com/questions/1822548/store-assign-roles-of-authenticated-users
            IPrincipal ip = HttpContext.Current.User;
            if (ip != null)
            {
                Customer customer = repository.GetByLogin(ip.Identity.Name);
                HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(ip.Identity, new string[] { customer.Role == null ? "" : customer.Role.ToString() });
            }
        }
    }
}
