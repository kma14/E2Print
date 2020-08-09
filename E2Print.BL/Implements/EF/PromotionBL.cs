using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain;
using E2Print.BL.Interfaces;
using E2Print.DAL;
using System.Data.Entity.Validation;

namespace E2Print.BL.Implements.EF
{
    public class PromotionBL:IPromotion
    {
        E2printEntities e2printEntities = new E2printEntities();
        public Domain.Entities.Promotion CreatePromotion(Domain.Entities.Promotion newPromotion)
        {
            DAL.Promotion dalPromotion = new DAL.Promotion();
            dalPromotion.ItemId = newPromotion.ItemId;
            dalPromotion.Title = newPromotion.Title;
            dalPromotion.Description = newPromotion.Description;
            dalPromotion.StartDate = newPromotion.StartDate;
            dalPromotion.EndDate = newPromotion.EndDate;
            dalPromotion.Comment = newPromotion.Comment;
            dalPromotion.DiscountAmount = newPromotion.DiscountAmount;
            dalPromotion.PromotionPrice = newPromotion.PromotionPrice;
            dalPromotion.PromotionGroup = newPromotion.PromotionGroup;
            dalPromotion.PromotionLink = newPromotion.PromotionLink;

            e2printEntities.Promotions.Add(dalPromotion);
            try
            {
                e2printEntities.SaveChanges();
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
            newPromotion.Id = dalPromotion.Id;
            return newPromotion;
        }

        public void DeletePromotion(int promotionId)
        {
            DAL.Promotion dalPromotion = e2printEntities.Promotions.Where(c => c.Id == promotionId).FirstOrDefault();
            e2printEntities.Promotions.Remove(dalPromotion);
            e2printEntities.SaveChanges();
        }

        public void UpdatePromotion(Domain.Entities.Promotion promotion)
        {
            DAL.Promotion dalPromotion = e2printEntities.Promotions.Where(c => c.Id == promotion.Id).FirstOrDefault();
            dalPromotion.ItemId = promotion.ItemId;
            dalPromotion.Title = promotion.Title;
            dalPromotion.Description = promotion.Description;
            dalPromotion.StartDate = promotion.StartDate;
            dalPromotion.EndDate = promotion.EndDate;
            dalPromotion.Comment = promotion.Comment;
            dalPromotion.DiscountAmount = promotion.DiscountAmount;
            dalPromotion.PromotionPrice = promotion.PromotionPrice;
            dalPromotion.PromotionGroup = promotion.PromotionGroup;
            dalPromotion.PromotionLink = promotion.PromotionLink;
            e2printEntities.SaveChanges();
        }

        List<Domain.Entities.Promotion> IPromotion.GetAll()
        {
            return ModelMapping.MapPromotion(e2printEntities.Promotions).ToList();
        }

        Domain.Entities.Promotion IPromotion.GetById(int promotionId)
        {
            Domain.Entities.Promotion promotion = ModelMapping.MapPromotion(e2printEntities.Promotions.Where(c => c.Id == promotionId).FirstOrDefault());
            return promotion;
        }
    }
}
