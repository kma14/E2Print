using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;
using E2Print.WebUI.Models;

namespace E2Print.WebUI.Controllers
{
    public class PromotionController : Controller
    {
        ICategory categoryRepository;
        IPromotion promotionRepository;
        ITag tagRepo;
        public PromotionController(IPromotion promotionRepository, ICategory categoryRepository, ITag tagRepo)
        {
            this.promotionRepository = promotionRepository;
            this.categoryRepository = categoryRepository;
            this.tagRepo = tagRepo;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Promotions()
        {
            var promotions = promotionRepository.GetAll();
            return View(promotions);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewPromotion()
        {
            ViewData["PromotionTags"] = tagRepo.GetByType("Promotion");
            Promotion promotion = new Promotion();
            return View(promotion);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditPromotion(int promotionId)
        {
            ViewData["PromotionTags"] = tagRepo.GetByType("Promotion");
            var promotion = promotionRepository.GetById(promotionId);
            return View(promotion);
        }

        /* ================= Post actions ======================= */
        [HttpPost]
        public ActionResult Create(Promotion model)
        {
            try
            {
                model.StartDate = new DateTime(2020, 01, 01);
                model.EndDate = new DateTime(2030, 01, 01);
                model.Id = promotionRepository.CreatePromotion(model).Id;
                return RedirectToAction("Promotions", "Promotion", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("NewPromotion", "Promotion", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Promotion model)
        {
            try
            {
                model.StartDate = new DateTime(2020, 01, 01);
                model.EndDate = new DateTime(2030, 01, 01);
                promotionRepository.UpdatePromotion(model);
                ViewData["CurrentPromotion"] = model.Id;
                return RedirectToAction("Promotions", "Promotion", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("EditPromotion", "Promotion", model);
            }
        }
        /* ================= ajax actions ======================= */

        [HttpPost]
        public JsonResult UploadPhoto(HttpPostedFileBase photo, string newPhotoName)
        {
            try
            {
                string path = Server.MapPath("~/Content/Images/Promotions/");
                if (photo != null && photo.ContentLength > 0)
                {
                    string fileName = Path.Combine(path, newPhotoName);
                    photo.SaveAs(fileName);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
            return Json(new { Result = "OK", Message = "New photo added:" + newPhotoName });
        }

        [HttpPost]
        public JsonResult DeletePromotion(int id)
        {
            try
            {
                promotionRepository.DeletePromotion(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
