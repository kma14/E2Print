using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.DAL;
using E2Print.Domain.Entities;
using E2Print.BL.Interfaces;

namespace E2Print.BL.Implements.EF
{
    public class ProductAttributeBL:IProductAttribute
    {
        E2printEntities e2printEntities = new E2printEntities();
        public List<Domain.Entities.ProductAttribute> GetAll()
        {
            return ModelMapping.MapProductAttribute(e2printEntities.ProductAttributes).ToList();
        }

        public List<Domain.Entities.ProductAttribute> GetByAttributeName(string attrName)
        {
            List<Domain.Entities.ProductAttribute> productAttrs = ModelMapping.MapProductAttribute(e2printEntities.ProductAttributes.Where(c => c.AttributeName.Equals(attrName))).ToList();
            return productAttrs;
        }


        public Domain.Entities.ProductAttribute Create(Domain.Entities.ProductAttribute productAttribute)
        {
            DAL.ProductAttribute dalProductAttribute = new DAL.ProductAttribute();
            dalProductAttribute.AttributeName = productAttribute.AttributeName;
            dalProductAttribute.AttributeValue = productAttribute.AttributeValue;
            dalProductAttribute.Description = productAttribute.Description;
            e2printEntities.ProductAttributes.Add(dalProductAttribute);
            e2printEntities.SaveChanges();
            productAttribute.Id = dalProductAttribute.Id;
            return productAttribute;
        }

        public void Delete(int productAttributeId)
        {
            DAL.ProductAttribute dalProductAttribute = e2printEntities.ProductAttributes.Where(c => c.Id == productAttributeId).FirstOrDefault();
            e2printEntities.ProductAttributes.Remove(dalProductAttribute);
            e2printEntities.SaveChanges();
        }

        public void DeleteByAttributeName(string attributeName)
        {
            var dalProductAttributes = e2printEntities.ProductAttributes.Where(c => c.AttributeName.Contains(attributeName.Trim()));
            foreach (DAL.ProductAttribute pa in dalProductAttributes)
            {
                e2printEntities.ProductAttributes.Remove(pa);
            }
            e2printEntities.SaveChanges();
        }

        public void Update(Domain.Entities.ProductAttribute productAttribute)
        {
            DAL.ProductAttribute dalProductAttribute = e2printEntities.ProductAttributes.Where(c => c.Id == productAttribute.Id).FirstOrDefault();
            dalProductAttribute.AttributeName = productAttribute.AttributeName;
            dalProductAttribute.AttributeValue = productAttribute.AttributeValue;
            dalProductAttribute.Description = productAttribute.Description;
            e2printEntities.SaveChanges();
        }


    }
}
