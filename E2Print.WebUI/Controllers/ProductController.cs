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
    public class ProductController : Controller
    {
        IProduct productRepository;
        ICategory categoryRepository;
        IProductAttribute paRepository;
        public ProductController(IProduct repository, ICategory categoryRepository, IProductAttribute paRepository)
        {
            this.productRepository = repository;
            this.categoryRepository = categoryRepository;
            this.paRepository = paRepository;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProductList(int jtStartIndex, int jtPageSize)
        {
            try
            {
                int productsCount = productRepository.GetProductCount();
                var pageProducts = productRepository.GetProducts(jtStartIndex, jtPageSize);
                var productGridModel = pageProducts.Select(c =>
                    new ProductViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Color=c.Color,
                        Size=c.Size,
                        Material = c.Material,
                        BuyingQty = c.BuyingQty,
                        Price = c.Price,
                        CategoryId = c.Category.Id,
                        CategoryName=c.Category.Name
                    });
                return Json(new { Result = "OK", Records = productGridModel, TotalRecordCount = productsCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult CreateProduct(ProductViewModel model)
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
                Category category = categoryRepository.GetById(model.CategoryId);
                Product newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Color=model.Color;
                newProduct.Size=model.Size;
                newProduct.Material = model.Material;
                newProduct.BuyingQty = model.BuyingQty;
                newProduct.Price = model.Price;
                newProduct.Category = category;
                model.Id = productRepository.Create(newProduct).Id;
                return Json(new { Result = "OK", Record = model });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("NewProduct", "Admin", model);
                }
                Category category = categoryRepository.GetById(model.CategoryId);
                Product newProduct = new Product();
                newProduct.Name = model.Name;
                newProduct.Description = model.Description;
                newProduct.Color = model.Color;
                newProduct.Size = model.Size;
                newProduct.Material = model.Material;
                newProduct.BuyingQty = model.BuyingQty;
                newProduct.Price = model.Price;
                newProduct.Category = category;
                model.Id = productRepository.Create(newProduct).Id;
                return RedirectToAction("ProductSaved", "Admin", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("NewProduct", "Admin", model);
            }
        }

        [HttpPost]
        public JsonResult UpdateProduct(ProductViewModel model)
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
                Category newCategory = categoryRepository.GetById(int.Parse(Request.Form["CategoryId"]));
                Product productToEdit = new Product();
                productToEdit.Id = model.Id;
                productToEdit.Name = model.Name;
                productToEdit.Description = model.Description;
                productToEdit.Color = model.Color;
                productToEdit.Size = model.Size;
                productToEdit.Material = model.Material;
                productToEdit.BuyingQty = model.BuyingQty;
                productToEdit.Price = model.Price;
                productToEdit.Category = newCategory;
                productRepository.Update(productToEdit);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("EditProduct", "Admin", model);
                }
                Category newCategory = categoryRepository.GetById(int.Parse(Request.Form["CategoryId"]));
                Product productToEdit = new Product();
                productToEdit.Id = model.Id;
                productToEdit.Name = model.Name;
                productToEdit.Description = model.Description;
                productToEdit.Color = model.Color;
                productToEdit.Size = model.Size;
                productToEdit.Material = model.Material;
                productToEdit.BuyingQty = model.BuyingQty;
                productToEdit.Price = model.Price;
                productToEdit.Category = newCategory;
                productRepository.Update(productToEdit);
                return RedirectToAction("ProductSaved", "Admin", model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("EditProduct", "Admin", model);
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct(int productId)
        {
            try
            {
                productRepository.Delete(productId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public ActionResult Detail()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageProduct()
        {
            List<string> a = paRepository.GetAll().GroupBy(c => c.AttributeName).Select(grp => grp.Key).ToList();
            SelectListViewModel<ProductAttribute> viewModel = new SelectListViewModel<ProductAttribute>
            {
                Keys = a,
                Items = paRepository.GetAll()
            };
            return View(viewModel);
        }
        [HttpPost]
        public JsonResult CreateProductAttribute(ProductAttribute model)
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
                ProductAttribute newAttribute = new ProductAttribute();
                newAttribute.AttributeName = model.AttributeName;
                newAttribute.AttributeValue = model.AttributeValue;
                newAttribute.Description = model.Description;
                model.Id = paRepository.Create(newAttribute).Id;
                return Json(new { Result = "OK", Record = model });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteProductAttributeById(int attributeId)
        {
            try
            {
                paRepository.Delete(attributeId);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteProductAttributeByName(string AttributeName)
        {
            try
            {
                paRepository.DeleteByAttributeName(AttributeName);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}