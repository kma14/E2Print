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
    public class FeedbackController : Controller
    {
        IFeedback feedbackRepository;

        public FeedbackController(IFeedback feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }
        public ActionResult CustomerFeedbacks()
        {
            return View(feedbackRepository.GetAll());
        }
        public ActionResult ManageFeedback()
        {
            return View();
        }

        /* ================= ajax actions ======================= */
        #region ajax actions from jTable grid

        [HttpPost]
        public JsonResult GetAll(int jtStartIndex, int jtPageSize)
        {
            try
            {
                var feedbacks = feedbackRepository.GetAll();
                int feedbackCount = feedbacks.Count();
                var pageFeedbacks = feedbacks.Skip(jtStartIndex).Take(jtPageSize);
                return Json(new { Result = "OK", Records = pageFeedbacks, TotalRecordCount = feedbackCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Create(CustomerFeedback feedback)
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
                var newFeedback = feedbackRepository.Create(feedback);
                return Json(new { Result = "OK", Record = newFeedback });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Update(CustomerFeedback feedback)
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
                feedbackRepository.Update(feedback);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(CustomerFeedback feedback)
        {
            try
            {
                feedbackRepository.Delete(feedback.Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion
    }
}