using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Interfaces
{
    public interface IPromotion
    {
        Promotion CreatePromotion(Promotion newPromotion);
        void DeletePromotion(int promotionId);
        void UpdatePromotion(Promotion promotion);
        List<Promotion> GetAll();

        Promotion GetById(int promotionId);
    }
}
