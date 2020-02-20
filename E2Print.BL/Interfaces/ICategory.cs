using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Interfaces
{
    public interface ICategory
    {
        List<Category> GetAll();
        Category GetById(int categoryId);
        List<Category> GetRootCategories();
        List<Category> GetLeafCategories();
        List<Category> GetChildrenCategories(int categoryId);

        Category Create(Category category);

        void Delete(int categoryId);

        void Update(Category category);
        List<string> GetAttributeList(List<Product> products, Func<Product, string> keySelector);

        void DeletePhoto(string fileUrl);
    }
}
