using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Interfaces
{
    public interface IFeedback
    {
        CustomerFeedback Create(CustomerFeedback feedback);
        void Update(CustomerFeedback feedback);

        void Delete(int feedbackId);

        List<CustomerFeedback> GetAll();
    }
}
