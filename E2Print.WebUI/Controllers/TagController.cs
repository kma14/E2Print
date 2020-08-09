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
    public class TagController : Controller
    {
        ITag tagRepo;
        public TagController(ITag tagRepo)
        {
            this.tagRepo = tagRepo;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Tags()
        {
            var tags = tagRepo.GetAll();
            return View(tags);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewTag()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditTag(Guid tagId)
        {
            var tag = tagRepo.GetById(tagId);
            return View(tag);
        }

        /* ================= Post actions ======================= */
        [HttpPost]
        public ActionResult Create(Tag model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                model.CreatedOn = DateTime.Now;
                tagRepo.Create(model);
                return RedirectToAction("Tags", "Tag", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("NewTag", "Tag", model);
            }
        }

        [HttpPost]
        public ActionResult Update(Tag model)
        {
            try
            {
                tagRepo.Update(model);
                ViewData["CurrentTag"] = model.Id;
                return RedirectToAction("Tags", "Tag", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("EditPromotion", "Tag", model);
            }
        }
        /* ================= ajax actions ======================= */

        [HttpPost]
        public JsonResult DeleteTag(Guid id)
        {
            try
            {
                tagRepo.Delete(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
