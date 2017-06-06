using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;


namespace E2Print.BL.Interfaces
{
    public interface IProductAttribute
    {
        List<ProductAttribute> GetAll();
        List<ProductAttribute> GetByAttributeName(string attrName);

        ProductAttribute Create(ProductAttribute productAttribute);

        void Delete(int productAttributeId);

        void DeleteByAttributeName(string attributeName);

        void Update(ProductAttribute productAttribute);
    }
}
