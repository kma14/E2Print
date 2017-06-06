using System;
using System.Collections.Generic;
using System.Linq;
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
        public PromotionController(IPromotion promotionRepository, ICategory categoryRepository)
        {
            this.promotionRepository = promotionRepository;
            this.categoryRepository = categoryRepository;
        }
        // GET: Promotion
        public ActionResult ManagePromotion()
        {
            return View();
        }

        public ActionResult SpecialOffers()
        {
            return View(promotionRepository.GetAll());
        }

        /* ================= ajax actions ======================= */
        [HttpPost]
        public JsonResult GetAllPromotions(int jtStartIndex, int jtPageSize)
        {
            try
            {
                var promotions = promotionRepository.GetAll();
                int promotionCount = promotions.Count();
                var pagePromotions = promotions.Skip(jtStartIndex).Take(jtPageSize);
                List<PromotionViewModel> promotionViewModes=new List<PromotionViewModel> ();
                foreach (Promotion p in pagePromotions)
                {
                    promotionViewModes.Add(new PromotionViewModel
                    {
                        Id = p.Id,
                        ItemId = p.ItemId,
                        ItemName =  categoryRepository.GetById(p.ItemId).Name,
                        Title = p.Title,
                        Description = p.Description,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate,
                        Comment = p.Comment,
                        DiscountAmount = p.DiscountAmount,
                        PromotionPrice = p.PromotionPrice
                    });
                }
                return Json(new { Result = "OK", Records = promotionViewModes, TotalRecordCount = promotionCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreatePromotion(Promotion promotion)
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

                var newRole = promotionRepository.CreatePromotion(promotion);
                return Json(new { Result = "OK", Record = newRole });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult UpdatePromotion(Promotion promotion)
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
                promotionRepository.UpdatePromotion(promotion);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeletePromotion(Promotion promotion)
        {
            try
            {
                promotionRepository.DeletePromotion(promotion.Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
