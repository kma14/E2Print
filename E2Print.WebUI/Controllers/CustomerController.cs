using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E2Print.Domain.Entities;
using E2Print.BL.Interfaces;
using System.Web.Security;
using E2Print.WebUI.Models;

namespace E2Print.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomer repository;
        private IUserRole roleRepository;
        public CustomerController(ICustomer customerRepository, IUserRole roleRepository)
        {
            this.repository = customerRepository;
            this.roleRepository = roleRepository;
        }
        public ViewResult List()
        {
            return View(repository.GetAll());
        }

        [HttpPost]
        public JsonResult GetUserList()
        {
            try
            {
                List<Customer> users = repository.GetAll().ToList();
                return Json(new { Result = "OK", Records = users });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUser(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "ERROR",
                        Message = "Form is not valid! " + "Please correct it and try again."
                    });
                }

                repository.Update(customer);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public ViewResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            if (TempData["model"] != null)
            {
                model = (LoginViewModel)TempData["model"];
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string login = model.UserName;
                string password = model.Password;
                Result result = repository.Authenticate(login, password);
                if (result.Succeeded)
                {
                    FormsAuthentication.SetAuthCookie(login, false);
                    return RedirectToAction("DashBoard", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    TempData["model"] = model;
                    TempData["viewData"] = ViewData;
                    return RedirectToAction("login"); //post-process-redirect to provent resubmit issue when user refresh page
                }
            }
            else
            {
                TempData["model"]=model;
                TempData["viewData"] = ViewData;
                return RedirectToAction("login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ViewResult SignUp()
        {
            Customer model = new Customer();
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            if (TempData["model"] != null)
            {
                model = (Customer)TempData["model"];
            }
            return View(model);
        }

        public ViewResult SignUpSucceeded()
        {
            ViewBag.NewCustomerId = (string)TempData["NewCustomerId"];
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                Result result = repository.Create(customer.Login, customer.Password, customer.FirstName, customer.LastName, customer.ContactNumber, customer.Address, customer.Email, customer.Company);
                if (result.Succeeded)
                {
                    TempData["NewCustomerId"] = result.Message;
                    FormsAuthentication.SetAuthCookie(customer.Login, false);
                    return RedirectToAction("SignUpSucceeded"); //http 302 temporary redirect 
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("SignUp"); //http 302 temporary redirect 
                }
            }
            else
            {
                TempData["model"] = customer;
                TempData["viewData"] = ViewData;
                return RedirectToAction("SignUp");
            }
        }

        public ActionResult ManageUser()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult MemberPanel()
        {
            //List<UserRole> roles = roleRepository.GetAll();
            //string roleList =string.Join(",", roles.Select(c => c.RoleName).ToArray());

            if (User.Identity.IsAuthenticated)
            {
                Customer customer = repository.GetByLogin(User.Identity.Name);
                UserRole userRole = roleRepository.GetByName(customer.Role);
                ViewBag.Discount = userRole.Discount;
                return PartialView("_MemberPanel", customer);
            }
            ViewBag.Discount = 1;
            return PartialView("_SignIn");
        }

        [HttpPost]
        public JsonResult GetCustomersByRole(string roleName)
        {
            try
            {
                List<Customer> customers = repository.GetByRole(roleName).ToList();
                return Json(new { Result = "OK", Records = customers });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}