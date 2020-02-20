using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.DAL;
using E2Print.Domain.Entities;
using E2Print.BL.Interfaces;
using System.IO;

namespace E2Print.BL.Implements.EF
{
    public class CategoryBL:ICategory
    {
        E2printEntities e2printEntities = new E2printEntities();
        public List<Domain.Entities.Category> GetAll()
        {
            return ModelMapping.MapCategory(e2printEntities.Categories).ToList();
        }

        public Domain.Entities.Category GetById(int categoryId)
        {
            Domain.Entities.Category category = ModelMapping.MapCategory(e2printEntities.Categories.Where(c => c.Id == categoryId).FirstOrDefault());
            return category;
        }


        //currently only support 2 level categories
        public List<Domain.Entities.Category> GetRootCategories()
        { 
            var rootCategories = ModelMapping.MapCategory(e2printEntities.Categories.Where(c => c.ParentId == null)).ToList();
            foreach(Domain.Entities.Category cat in rootCategories)
            {
                cat.SubCategories = GetChildrenCategories(cat.Id);
            }
            return rootCategories;
        }

        public List<Domain.Entities.Category> GetChildrenCategories(int categoryId)
        {
            return ModelMapping.MapCategory(e2printEntities.Categories.Where(c => c.ParentId == categoryId)).ToList();
        }


        public Domain.Entities.Category Create(Domain.Entities.Category category)
        {
            DAL.Category dalCategory = new DAL.Category();
            dalCategory.Name = category.Name;
            dalCategory.Description = category.Description;
            dalCategory.ParentId = category.ParentId.Value == 0 ? (int?)null : category.ParentId.Value;
            e2printEntities.Categories.Add(dalCategory);
            e2printEntities.SaveChanges();
            category.Id = dalCategory.Id;
            return category;
        }

        public void Delete(int categoryId)
        {
            DAL.Category dalCategory = e2printEntities.Categories.FirstOrDefault(c => c.Id == categoryId);
            e2printEntities.Categories.Remove(dalCategory);
            e2printEntities.SaveChanges();
        }

        public void Update(Domain.Entities.Category category)
        {
            DAL.Category dalCategory = e2printEntities.Categories.Where(c => c.Id == category.Id).FirstOrDefault();
            dalCategory.Name = category.Name;
            dalCategory.Description = category.Description;
            dalCategory.ParentId = category.ParentId.Value == 0 ? (int?)null : category.ParentId.Value;
            e2printEntities.SaveChanges();
        }


        public List<Domain.Entities.Category> GetLeafCategories()
        {
            return ModelMapping.MapCategory(e2printEntities.Categories.Where(c => e2printEntities.Categories.Where(i => i.ParentId != null).Where(i => i.ParentId == c.Id).Count()==0)).ToList();
        }

        public List<string> GetAttributeList(List<Domain.Entities.Product> products,Func<Domain.Entities.Product,string> keySelector)
        {
            return products.GroupBy(keySelector).Select(grp => grp.Key).ToList<string>();
        }


        public void DeletePhoto(string fileUrl)
        {
            File.Delete(fileUrl);
        }
    }
}
