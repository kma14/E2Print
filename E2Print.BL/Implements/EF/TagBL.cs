using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.DAL;
using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;
using Tag = E2Print.Domain.Entities.Tag;

namespace E2Print.BL.Implements.EF
{
    public class TagBL:ITag
    {
        E2printEntities e2PrintEntities = new E2printEntities();

        public List<Tag> GetAll()
        {
            List<Tag> tags = new List<E2Print.Domain.Entities.Tag>();

            foreach (var dalTag in e2PrintEntities.Tags)
            {
                Tag tag = ModelMapping.MapTag(dalTag);
                tags.Add(tag);
            }
            return tags;
        }

        Tag ITag.GetById(Guid id)
        {
            var tag = e2PrintEntities.Tags.Where(c => c.Id == id).FirstOrDefault();
            return ModelMapping.MapTag(tag);
        }

        public Tag Create(Tag model)
        {
            DAL.Tag tag = new DAL.Tag {Id=model.Id, Name = model.Name, Type=model.Type, Description = model.Description,CreatedOn=model.CreatedOn };
            e2PrintEntities.Tags.Add(tag);
            e2PrintEntities.SaveChanges();
            return ModelMapping.MapTag(tag);
        }

        public Result Update(Tag model)
        {
            Result result = new Result();
            result.Succeeded = true;

            DAL.Tag tag = e2PrintEntities.Tags.Where(c => c.Id == model.Id).FirstOrDefault();
            tag.Id = model.Id;
            tag.Type = model.Type;
            tag.Name = model.Name;
            tag.Description = model.Description;
            try
            {
                e2PrintEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        //http://stackoverflow.com/questions/17723626/entity-framework-remove-vs-deleteobject
        public Result Delete(Guid id) 
        {
            Result result = new Result();
            result.Succeeded = true;

            DAL.Tag tag = e2PrintEntities.Tags.Where(c => c.Id == id).FirstOrDefault();
            try
            {
                e2PrintEntities.Tags.Remove(tag);
                e2PrintEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public List<Tag> GetByType(string typeName)
        {
            List<Tag> tags = new List<Tag>();

            foreach (var dalTag in e2PrintEntities.Tags.Where(c=>c.Type == typeName))
            {
                Tag tag = ModelMapping.MapTag(dalTag);
                tags.Add(tag);
            }
            return tags;
        }
    }
}
