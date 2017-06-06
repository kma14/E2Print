using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;

namespace E2Print.WebUI.Controllers
{
    public class PhotoController : Controller
    {
        string itemNumber;
        string path;

        ICategory categoryRepository;
        public PhotoController(ICategory categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        // GET: Photo
        public ActionResult ManagePhoto()
        {

            return View(categoryRepository.GetAll().ToList());
        }

        public void UploadPhoto()
        {
            itemNumber = "E2Print";
            //SavePhoto(itemNumber, Request);
            SavePhoto_JqueryFileUpload(Request);
        }
        private void SavePhoto_JqueryFileUpload(HttpRequestBase request)
        {
            HttpFileCollectionBase files = request.Files;
            switch (Request.Form["photoType"])
            {
                case "1":
                    path += Server.MapPath("~/Content/Images/Items/");
                    break;
                case "2":
                    path += Server.MapPath("~/Content/Images/ItemHomePage/");
                    break;
                case "3":
                    path += Server.MapPath("~/Content/Images/Promotions/");
                    break;
                case "4":
                    path += Server.MapPath("~/Content/Images/PromotionHomePage/");
                    break;
                case "5": //homepage background image
                    path += Server.MapPath("~/Content/Images/");
                    break;
                case "6": //feedback image
                    path += Server.MapPath("~/Content/Images/Feedbacks/");
                    break;
            }
            foreach(string key in files.AllKeys)
            {
                HttpPostedFileBase postedFile = request.Files[key];
                string fileExtension;
                string categoryId = Request.Form["category"];

                string postedFileName = Path.GetFileName(postedFile.FileName);
                int filesize = postedFile.ContentLength;

                if (!string.IsNullOrEmpty(postedFileName))
                {
                    fileExtension = Path.GetExtension(postedFileName);
                    string filePath = path + categoryId + fileExtension;

                    if (Request.Form["photoType"].Equals("5"))
                    {
                        filePath = path + "background" + fileExtension;
                    }
                    else if (Request.Form["photoType"].Equals("6"))
                    {
                        filePath = path + Request.Form["feedbackId"] + fileExtension;
                    }
                    else if (Request.Form["photoType"].Equals("1"))
                    {
                        int fileCount = 0;
                        while (System.IO.File.Exists(path + categoryId + "_" + fileCount.ToString() + fileExtension))
                        {
                            fileCount++;
                        }
                        filePath = path + categoryId + "_" + fileCount.ToString() + fileExtension;
                    }
                    postedFile.SaveAs(filePath);
                }
                Response.Write(postedFileName + "上传成功");
            }
        }
        private void SavePhoto(string caseNumber,HttpRequestBase request)
        {
            HttpFileCollectionBase files = request.Files;
            switch (Request.Form["photoType"])
            {
                case "1":
                    path += Server.MapPath("~/Content/Images/Items/");
                    break;
                case "2":
                    path += Server.MapPath("~/Content/Images/ItemHomePage/");
                    break;
                case "3":
                    path += Server.MapPath("~/Content/Images/Promotions/");
                    break;
                case "4":
                    path += Server.MapPath("~/Content/Images/PromotionHomePage/");
                    break;

            }
            if (files.Count > 0)
            {
                HttpPostedFileBase postedFile = request.Files["Filedata"];
                string fileExtension;
                string oldFileName = Path.GetFileName(postedFile.FileName);
                int filesize = postedFile.ContentLength;
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    fileExtension = Path.GetExtension(oldFileName);
                    string filePath = path +Request.Form["category"]+ fileExtension;
                    if (Request.Form["photoType"].Equals("1"))
                    {
                        int fileCount = 0;
                        while (System.IO.File.Exists(path + Request.Form["category"] + "_" + fileCount.ToString() + fileExtension))
                        {
                            fileCount++;
                        }
                        filePath = path + Request.Form["category"] + "_" + fileCount.ToString() + fileExtension;
                    }
                    postedFile.SaveAs(filePath);
                }
                Response.Write(oldFileName + "上传成功");
            }
        }

        [HttpPost]
        public JsonResult DeletePhoto(string fileUrl)
        {
            try
            {
                string fileName = fileUrl.Split('/').Last();
                path = Server.MapPath("~/Content/Images/Items/");
                System.IO.File.Delete(path + fileName);
                return Json(new { Result = "OK", Message = "Photo Deleted" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}