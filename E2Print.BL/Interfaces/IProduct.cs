using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAll();

        List<Product> GetProducts(int startIndex, int pageSize);
        int GetProductCount();
        List<Product> GetByCategoryId(int categoryId);

        Product GetById(int productId);
        Product SearchByAttributes(string name,string color,string material, string size, int quantity);

        Product Create(Product product);
        Result Update(Product product);
        void Delete(int productId);
    }
}
