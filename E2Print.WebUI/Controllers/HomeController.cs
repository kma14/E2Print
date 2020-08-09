using E2Print.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E2Print.Domain.Entities;
using E2Print.WebUI.Models;
using System.Web.Helpers;

namespace E2Print.WebUI.Controllers
{
    public class HomeController : Controller
    {
        ICategory categoryRepository;
        IProduct productRepository;
        IPromotion promotionRepository;
        ITag tagRepo;
        public HomeController(ICategory categoryRepository, IProduct productRepository,IPromotion promotionRepository, ITag tagRepo)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.promotionRepository = promotionRepository;
            this.tagRepo = tagRepo;
        }
        public ActionResult Index()
        {
            //ViewBag.Title = "E2 Print - all kinds of printing";

            //HomeViewModel viewModel = new HomeViewModel();

            //var roots = categoryRepository.GetAll().Where(c => c.ParentId == null);
            //foreach(Category root in roots)
            //{
            //    root.SubCategories = categoryRepository.GetChildrenCategories(root.Id);
            //}

            //viewModel.Categories = roots.ToList();
            //viewModel.Promotions = promotionRepository.GetAll().Take(3).ToList();

            //return View(viewModel);
            ViewData["PromotionTags"] = tagRepo.GetByType("Promotion");
            var model = promotionRepository.GetAll();
            ViewData["Page"] = "Home";
            return View(model);
        }

        public ActionResult AboutUs()
        {
            ViewData["Page"] = "About";
            return View();
        }
        public ActionResult Equipment()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            ViewData["Page"] = "Contact";
            return View();
        }

        [HttpPost]
        public JsonResult  SendEmail()
        {
            //user two line breaks because emial provider tends to remove one line break from email content
            string emailContent = "</br></br>This is an automactic message from www.e2print.co.nz, please do not reply</br></br>";
            emailContent += "--------------------------------------------------------------------------------------------------------------------------------------------------</br></br>";
            emailContent += Request.Form["emailContent"] + "</br></br>";
            emailContent += "--------------------------------------------------------------------------------------------------------------------------------------------------</br></br>";
            emailContent += "User email:" + Request.Form["userEmail"] + "</br>";
            emailContent += "User phone:" + Request.Form["userCellphone"];

            WebMail.SmtpServer = "smtpout.asia.secureserver.net"; // income: pop.asia.secureserver.net
            WebMail.SmtpPort = 25;
            WebMail.UserName = "web@e2print.co.nz";
            WebMail.Password = "P@ssw0rd";
            WebMail.From = "web@e2print.co.nz";
            WebMail.EnableSsl = false;
            try
            {
                WebMail.Send("info@e2print.co.nz", "Website Message(" + System.DateTime.Now + ")", emailContent, null, "design@e2print.co.nz", null, true);
                return Json(new { Result = "succeed", Message = "Message sent! \n\nThank you for your enquiry\nWe will get back to you ASAP!" });
            }
            catch (Exception ex)
            {
                return Json(new {Result="error",Message="Error :( \n"+ex.Message});
            }
        }
    }
}
