using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;
using E2Print.WebUI.Models;
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
        ICategory categoryRepository;
        IProduct productRepository;
        IPromotion promotionRepository;
        public AdminController(ICategory categoryRepository, IProduct productRepository, IPromotion promotionRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.promotionRepository = promotionRepository;
        }


        public ActionResult DashBoard()
        {
            ViewBag.CategoryCount = categoryRepository.GetAll().Count;
            ViewBag.ProductCount = productRepository.GetProductCount();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Products(string productNumber)
        {
            List<Product> model = new List<Product>();
            if (string.IsNullOrEmpty(productNumber))
            {
                model = productRepository.GetAll();
            }
            //ProductListViewModel model = new ProductListViewModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewProduct()
        {
            var cats = categoryRepository.GetRootCategories();
            ProductViewModel model = new ProductViewModel();
            ViewData["Categories"] = cats;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditProduct(int productId)
        {
            var cats = categoryRepository.GetRootCategories();
            ViewData["Categories"] = cats;
            var product = productRepository.GetById(productId);
            ProductViewModel model = new ProductViewModel()
            {
                Id=product.Id,
                CategoryId = product.Category.Id,
                Name = product.Name,
                Description = product.Description,
                Size = product.Size,
                Color = product.Color,
                Material = product.Material,
                BuyingQty = product.BuyingQty,
                Price =product.Price 
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductSaved(ProductViewModel model)
        {
            return View(model);
        }
    }
}