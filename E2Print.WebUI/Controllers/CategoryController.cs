using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;
using E2Print.WebUI.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace E2Print.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategory categoryRepository;
        IProduct productRepository;
        IPromotion promotionRepository;
        public CategoryController(ICategory categoryRepository, IProduct productRepository, IPromotion promotionRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.promotionRepository = promotionRepository;
        }

        [HttpPost]
        public JsonResult GetAllCategories(int jtStartIndex, int jtPageSize)
        {
            try
            {
                var categories = categoryRepository.GetAll();
                int categoryCount = categories.Count();
                var pageCategories = categories.Skip(jtStartIndex).Take(jtPageSize);
                return Json(new { Result = "OK", Records = pageCategories, TotalRecordCount = categoryCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetLeafCategories_DropDownList() //for grid option selects
        {
            try
            {
                var categories = categoryRepository.GetLeafCategories().Select(c => new { DisplayText = c.Name, Value = c.Id }).ToList();
                return Json(new { Result = "OK", Options = categories });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCategoryById(int categoryId)
        {
            try
            {
                var categorie = categoryRepository.GetById(categoryId);
                return Json(new { Result = "OK", Data = categorie });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCategories_DropDownList(string id)
        {
            try
            {
                var categories = categoryRepository.GetAll().Select(c => new { DisplayText = c.Name, Value = c.Id }).ToList();
                categories.Add(new { DisplayText = "-- Root Category --", Value = 0 });
                return Json(new { Result = "OK", Options = categories });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Options = ex.Message });
            }
        }

        [ChildActionOnly]
        public PartialViewResult CategoryNavigation()
        {
            List<CategoryNavigation> cateNavs = new List<Models.CategoryNavigation> ();
            var categories = categoryRepository.GetAll().OrderBy(c => c.Name).ToList();
            var rootCategories = categoryRepository.GetRootCategories().OrderBy(c => c.Name);
            foreach(Category rootCategory in rootCategories){
                CategoryNavigation cateNav = new CategoryNavigation ();
                cateNav.Category = rootCategory;
                cateNav.SubCategories = categories.Where(c => c.ParentId == rootCategory.Id).ToList();
                cateNavs.Add(cateNav);
            }
            return PartialView(cateNavs);
        }

        [HttpPost]
        public JsonResult CreateCategory(Category category)
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

                var newRole = categoryRepository.Create(category);
                return Json(new { Result = "OK", Record = newRole });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateCategory(Category category)
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
                categoryRepository.Update(category);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteCategory(int categoryId)
        {
            try
            {
                
                var products = productRepository.GetAll().Where(c => c.Category.Id == categoryId);
                foreach(Product product in products){
                    product.Category=null;
                    productRepository.Update(product);
                }

                categoryRepository.Delete(categoryId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [ChildActionOnly]
        public ActionResult AllCategories(string customerId)
        {
            var cats = categoryRepository.GetRootCategories();
            return PartialView("_AllCategories", cats);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Categories()
        {
            var cats = categoryRepository.GetRootCategories();
            ViewData["Categories"] = cats;
            var categories = categoryRepository.GetAll();

            List<CategoryViewModel> model = categories.Select(c => new CategoryViewModel
            {
                Id=c.Id,
                Name=c.Name,
                Description=c.Description,
                ParentId = c.ParentId,
                ParentCategory = c.ParentId==null?null: categories.FirstOrDefault(cate=>cate.Id==c.ParentId)
            }).OrderBy(c=>c.ParentId).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewCategory()
        {
            var cats = categoryRepository.GetRootCategories();
            ViewData["Categories"] = cats;
            CategoryViewModel model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            try
            {
                Category newCategory = new Category();
                newCategory.ParentId = model.ParentId==null?0: model.ParentId.Value;
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                model.Id = categoryRepository.Create(newCategory).Id;
                return RedirectToAction("Categories", "Category", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("NewCategory", "Category", model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditCategory(int categoryId)
        {
            var cats = categoryRepository.GetRootCategories();
            ViewData["Categories"] = cats;
            var category = categoryRepository.GetById(categoryId);
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                ParentId = category.ParentId,
                Name = category.Name,
                Description = category.Description
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Update(CategoryViewModel model)
        {
            try
            {
                Category categoryToUpdate = new Category();
                categoryToUpdate.Id = model.Id;
                categoryToUpdate.ParentId = model .ParentId== null?0: model.ParentId.Value;
                categoryToUpdate.Name = model.Name;
                categoryToUpdate.Description = model.Description;
                categoryRepository.Update(categoryToUpdate);
                ViewData["CurrentCategory"] = categoryToUpdate.Id;
                return RedirectToAction("Categories", "Category", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("EditCategory", "Category", model);
            }
        }

        public ActionResult Detail(int id)
        {
            Category category = categoryRepository.GetById(id);
            Regex reg = new Regex(@"^.*" + id + "_[0-9]{1}$");
            string path = Server.MapPath("~/Content/Images/Items/");
            var photos = Directory.GetFiles(path).Where(f => reg.IsMatch(Path.GetFileNameWithoutExtension(f))).Select(c => "/Content/Images/Items/" + Path.GetFileName(c));
            List<Product> products = productRepository.GetByCategoryId(id);
            bool onsale = promotionRepository.GetAll().Where(c => c.ItemId == id).Count() > 0;
            CategoryAndProductsViewModel viewModel = new CategoryAndProductsViewModel
            {
                Category = category,
                OnSale = onsale,
                Discount = onsale ? promotionRepository.GetAll().Where(c => c.ItemId == id).First().DiscountAmount.Value : 1,
                Products = products,
                Photos = photos.Count() > 0 ? photos.ToList() : new List<string> { "" },
                Sizes = products.GroupBy(c => c.Size).Select(grp => grp.Key).ToList<string>(),
                Colors = products.GroupBy(c => c.Color).Select(grp => grp.Key).ToList<string>(),
                Materials = products.GroupBy(c => c.Material).Select(grp => grp.Key).ToList<string>(),
                Packs = products.GroupBy(c => c.BuyingQty).Select(grp => grp.Key.ToString()).ToList<string>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetCategoryPhoto(int categoryId)
        {
            try
            {
                Regex reg = new Regex(@"^.*" + categoryId + "_[0-9]{1}$");
                string path = Server.MapPath("~/Content/Images/Items/");
                var photos = Directory.GetFiles(path).Where(f => reg.IsMatch(Path.GetFileNameWithoutExtension(f))).Select(c => "/Content/Images/Items/" + Path.GetFileName(c));
                return Json(new { Result = "OK", Message = photos.Count() > 0 ? photos.ToList() : new List<string> { "" } });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeletePhoto(string fileUrl)
        {
            try
            {
                categoryRepository.DeletePhoto(fileUrl);
                return Json(new { Result = "OK", Message = "Photo Deleted" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}