using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Implements.EF
{
    public static class ModelMapping
    {
        public static E2Print.Domain.Entities.Customer MapCustomer(E2Print.DAL.Customer dalCustomer)
        {
            E2Print.Domain.Entities.Customer domainCustomer = new Domain.Entities.Customer();
            if (dalCustomer != null)
            {
                domainCustomer.Id = dalCustomer.Id;
                domainCustomer.Login = dalCustomer.Login;
                domainCustomer.FirstName = dalCustomer.FirstName;
                domainCustomer.LastName = dalCustomer.LastName;
                domainCustomer.Company = dalCustomer.Company;
                domainCustomer.Password = dalCustomer.Password;
                domainCustomer.Address = dalCustomer.Address;
                domainCustomer.Email = dalCustomer.Email;
                domainCustomer.Company = dalCustomer.Company;
                domainCustomer.Role = dalCustomer.Role;
            }
            return domainCustomer;
        }
        public static IEnumerable<E2Print.Domain.Entities.Customer> MapCustomer(IEnumerable<E2Print.DAL.Customer> dalCustomers)
        {
            List<E2Print.Domain.Entities.Customer> domainCustomers = new List<E2Print.Domain.Entities.Customer>();

            foreach (var dalCustomer in dalCustomers)
            {
                E2Print.Domain.Entities.Customer domainCustomer = MapCustomer(dalCustomer);
                domainCustomers.Add(domainCustomer);
            }
            return domainCustomers;
        }

        public static E2Print.Domain.Entities.UserRole MapRole(E2Print.DAL.Role role)
        {
            E2Print.Domain.Entities.UserRole userRole = new Domain.Entities.UserRole();
            userRole.Id = role.Id;
            userRole.RoleName = role.RoleName;
            userRole.Discount = role.Discount;
            return userRole;
        }

        public static E2Print.Domain.Entities.Product MapProduct(E2Print.DAL.Product dalProduct,bool lazyLoading=false)
        {
            CategoryBL categoryRepository = new  CategoryBL();
            E2Print.Domain.Entities.Product product = new Domain.Entities.Product();
            product.Id = dalProduct.Id;
            product.Name = dalProduct.Name;
            product.Description = dalProduct.Description;
            product.Size = dalProduct.Size;
            product.Color = dalProduct.Color;
            product.Material = dalProduct.Material;
            product.BuyingQty = dalProduct.BuyingQty.Value;
            product.Price = dalProduct.Price;
            if(dalProduct.CategoryId == null)
            {
                product.Category = null;
            }
            else 
            if (!lazyLoading && dalProduct.CategoryId != null)
            {
                product.Category = categoryRepository.GetById(dalProduct.CategoryId.Value);
            }
            else
            {
                product.Category = new Category()
                {
                    Id= dalProduct.CategoryId.Value
                };
            }
            return product;
        }

        public static IEnumerable<E2Print.Domain.Entities.Product> MapProduct(IEnumerable<E2Print.DAL.Product> dalProducts, bool lazyLoading = false)
        {
            List<E2Print.Domain.Entities.Product> domainProducts = new List<E2Print.Domain.Entities.Product>();

            foreach (var dalProduct in dalProducts)
            {
                Product domainProduct = MapProduct(dalProduct, lazyLoading);
                domainProducts.Add(domainProduct);
            }
            return domainProducts;
        }

        public static E2Print.Domain.Entities.Category MapCategory(E2Print.DAL.Category dalCategory)
        {
            E2Print.Domain.Entities.Category Category = new Domain.Entities.Category();
            Category.Id = dalCategory.Id;
            Category.Name = dalCategory.Name;
            Category.Description = dalCategory.Description;
            Category.ParentId = dalCategory.ParentId;
            return Category;
        }

        public static IEnumerable<E2Print.Domain.Entities.Category> MapCategory(IEnumerable<E2Print.DAL.Category> dalCategorys)
        {
            List<E2Print.Domain.Entities.Category> domainCategorys = new List<E2Print.Domain.Entities.Category>();

            foreach (var dalCategory in dalCategorys)
            {
                E2Print.Domain.Entities.Category domainCategory = MapCategory(dalCategory);
                domainCategorys.Add(domainCategory);
            }
            return domainCategorys;
        }

        public static E2Print.Domain.Entities.Promotion MapPromotion(E2Print.DAL.Promotion dalPromotion)
        {
            E2Print.Domain.Entities.Promotion customerFeedback = new Domain.Entities.Promotion();
            customerFeedback.Id = dalPromotion.Id;
            customerFeedback.ItemId = dalPromotion.ItemId;
            customerFeedback.Title = dalPromotion.Title;
            customerFeedback.Description = dalPromotion.Description;
            customerFeedback.StartDate = dalPromotion.StartDate;
            customerFeedback.EndDate = dalPromotion.EndDate;
            customerFeedback.Comment = dalPromotion.Comment;
            customerFeedback.DiscountAmount = dalPromotion.DiscountAmount;
            customerFeedback.PromotionPrice = dalPromotion.PromotionPrice;
            return customerFeedback;
        }

        public static IEnumerable<E2Print.Domain.Entities.Promotion> MapPromotion(IEnumerable<E2Print.DAL.Promotion> dalPromotions)
        {
            List<E2Print.Domain.Entities.Promotion> domainPromotions = new List<E2Print.Domain.Entities.Promotion>();

            foreach (var dalPromotion in dalPromotions)
            {
                E2Print.Domain.Entities.Promotion domainPromotion = MapPromotion(dalPromotion);
                domainPromotions.Add(domainPromotion);
            }
            return domainPromotions;
        }

        public static E2Print.Domain.Entities.CustomerFeedback MapCustomerFeedback(E2Print.DAL.CustomerFeedback dalCustomerFeedback)
        {
            E2Print.Domain.Entities.CustomerFeedback customerFeedback = new Domain.Entities.CustomerFeedback();
            customerFeedback.Id = dalCustomerFeedback.Id;
            customerFeedback.Comment = dalCustomerFeedback.Comment;
            customerFeedback.CommentDate = dalCustomerFeedback.CommentDate;
            customerFeedback.CustomerName = dalCustomerFeedback.CustomerName;
            customerFeedback.CustomerId = dalCustomerFeedback.CustomerId;
            customerFeedback.Photo = dalCustomerFeedback.Photo;
            return customerFeedback;
        }

        public static IEnumerable<E2Print.Domain.Entities.CustomerFeedback> MapCustomerFeedback(IEnumerable<E2Print.DAL.CustomerFeedback> dalCustomerFeedbacks)
        {
            List<E2Print.Domain.Entities.CustomerFeedback> domainCustomerFeedbacks = new List<E2Print.Domain.Entities.CustomerFeedback>();

            foreach (var dalCustomerFeedback in dalCustomerFeedbacks)
            {
                E2Print.Domain.Entities.CustomerFeedback domainCustomerFeedback = MapCustomerFeedback(dalCustomerFeedback);
                domainCustomerFeedbacks.Add(domainCustomerFeedback);
            }
            return domainCustomerFeedbacks;
        }

        public static E2Print.Domain.Entities.ProductAttribute MapProductAttribute(E2Print.DAL.ProductAttribute dalProductAttribute)
        {
            E2Print.Domain.Entities.ProductAttribute customerFeedback = new Domain.Entities.ProductAttribute();
            customerFeedback.Id = dalProductAttribute.Id;
            customerFeedback.AttributeName = dalProductAttribute.AttributeName;
            customerFeedback.AttributeValue = dalProductAttribute.AttributeValue;
            customerFeedback.Description = dalProductAttribute.Description;
            return customerFeedback;
        }

        public static IEnumerable<E2Print.Domain.Entities.ProductAttribute> MapProductAttribute(IEnumerable<E2Print.DAL.ProductAttribute> dalProductAttributes)
        {
            List<E2Print.Domain.Entities.ProductAttribute> domainProductAttributes = new List<E2Print.Domain.Entities.ProductAttribute>();

            foreach (var dalProductAttribute in dalProductAttributes)
            {
                E2Print.Domain.Entities.ProductAttribute domainProductAttribute = MapProductAttribute(dalProductAttribute);
                domainProductAttributes.Add(domainProductAttribute);
            }
            return domainProductAttributes;
        }
    }
}
