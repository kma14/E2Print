using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.DAL;
using E2Print.Domain.Entities;
using E2Print.BL.Interfaces;
using System.Data.Entity.Validation;

namespace E2Print.BL.Implements.EF
{
    public class ProductBL:IProduct
    {
        E2printEntities e2PrintEntities = new E2printEntities();
        public List<Domain.Entities.Product> GetAll()
        {
            var products = ModelMapping.MapProduct(e2PrintEntities.Products, true).Where(c=>c.Category!=null).ToList();
            var categories = e2PrintEntities.Categories.ToList();
            foreach(Domain.Entities.Product p in products)
            {
                p.Category.Name = categories.FirstOrDefault(c => c.Id == p.Category.Id)?.Name;
            }

            return products;
        }

        public Domain.Entities.Product GetById()
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Product SearchByAttributes(string name, string color, string material, string size, int quantity)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Product Create(Domain.Entities.Product product)
        {
            DAL.Product dalProduct = new DAL.Product();
            dalProduct.Name = product.Name;
            dalProduct.Description = product.Description;
            dalProduct.Size = product.Size;
            dalProduct.Material = product.Material;
            dalProduct.Color = product.Color;
            dalProduct.BuyingQty = product.BuyingQty;
            dalProduct.Price = product.Price;
            dalProduct.CategoryId = product.Category.Id;

            e2PrintEntities.Products.Add(dalProduct);
            e2PrintEntities.SaveChanges();

            product.Id = dalProduct.Id;
            return product;
        }

        public Result Update(Domain.Entities.Product product)
        {
            Result result = new Result();
            result.Succeeded = true;

            try
            {
                DAL.Product dalProduct = e2PrintEntities.Products.Where(c => c.Id == product.Id).FirstOrDefault();
                dalProduct.Name = product.Name;
                dalProduct.Description = product.Description;
                dalProduct.Size = product.Size;
                dalProduct.Material = product.Material;
                dalProduct.Color = product.Color;
                dalProduct.BuyingQty = product.BuyingQty;
                dalProduct.Price = product.Price;
                dalProduct.CategoryId = product.Category == null ? (int?)null : product.Category.Id;

                e2PrintEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {

                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public void Delete(int productId)
        {
            DAL.Product dalProduct = e2PrintEntities.Products.Where(c => c.Id == productId).FirstOrDefault();
            e2PrintEntities.Products.Remove(dalProduct);
            e2PrintEntities.SaveChanges();
        }


        public List<Domain.Entities.Product> GetByCategoryId(int categoryId)
        {
            return ModelMapping.MapProduct(e2PrintEntities.Products.Where(c=>c.CategoryId == categoryId)).ToList();
        }


        public List<Domain.Entities.Product> GetProducts(int startIndex, int pageSize)
        {
            return ModelMapping.MapProduct(e2PrintEntities.Products.OrderBy(c=>c.CategoryId).Skip(startIndex).Take(pageSize)).ToList();
        }


        public int GetProductCount()
        {
            return e2PrintEntities.Products.Count();
        }

        public Domain.Entities.Product GetById(int productId)
        {
            return ModelMapping.MapProduct(e2PrintEntities.Products.FirstOrDefault(c => c.Id == productId));
        }
    }
}
