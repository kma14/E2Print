using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.BL.Interfaces;
using E2Print.DAL;

namespace E2Print.BL.Implements.EF
{
    public class FeedbackBL:IFeedback
    {
        E2printEntities e2printEntities = new E2printEntities();
        public Domain.Entities.CustomerFeedback Create(Domain.Entities.CustomerFeedback customerFeedback)
        {
            DAL.CustomerFeedback dalCustomerFeedback = new DAL.CustomerFeedback();
            dalCustomerFeedback.Comment = customerFeedback.Comment;
            dalCustomerFeedback.CommentDate = customerFeedback.CommentDate;
            dalCustomerFeedback.CustomerName = customerFeedback.CustomerName;
            dalCustomerFeedback.CustomerId = customerFeedback.CustomerId;
            dalCustomerFeedback.Photo = customerFeedback.Photo;
            e2printEntities.CustomerFeedbacks.Add(dalCustomerFeedback);
            e2printEntities.SaveChanges();
            customerFeedback.Id = dalCustomerFeedback.Id;
            return customerFeedback;
        }

        public void Update(Domain.Entities.CustomerFeedback customerFeedback)
        {
            DAL.CustomerFeedback dalCustomerFeedback = e2printEntities.CustomerFeedbacks.Where(c => c.Id == customerFeedback.Id).FirstOrDefault();
            dalCustomerFeedback.Comment = customerFeedback.Comment;
            dalCustomerFeedback.CommentDate = System.DateTime.Today;
            dalCustomerFeedback.CustomerName = customerFeedback.CustomerName;
            dalCustomerFeedback.CustomerId = customerFeedback.CustomerId;
            dalCustomerFeedback.Photo = customerFeedback.Photo;
            e2printEntities.SaveChanges();
        }

        public void Delete(int feedbackId)
        {
            DAL.CustomerFeedback dalCustomerFeedback = e2printEntities.CustomerFeedbacks.Where(c => c.Id == feedbackId).FirstOrDefault();
            e2printEntities.CustomerFeedbacks.Remove(dalCustomerFeedback);
            e2printEntities.SaveChanges();
        }

        public List<Domain.Entities.CustomerFeedback> GetAll()
        {
            return ModelMapping.MapCustomerFeedback(e2printEntities.CustomerFeedbacks).ToList();
        }
    }
}
