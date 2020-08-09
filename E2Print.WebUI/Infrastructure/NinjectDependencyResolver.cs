using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using E2Print.Domain.Entities;
using E2Print.BL.Interfaces;
using E2Print.BL.Implements.EF;
using Moq;

namespace E2Print.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            Mock<ICustomer> mock = new Mock<ICustomer>();
            mock.Setup(m => m.GetAll()).Returns(new List<Customer> {
                new Customer { Id=1001,FirstName="Kevin",LastName="Ma",Email="km@abc.com",Login="kevin",Password="password",Role="Admin",ContactNumber="0213456789",Address="1/5 wolverton st, avondale, auckland"},
                new Customer { Id=1002,FirstName="Ass",LastName="Fan",Email="km@abc.com",Login="pigu",Password="password",Role="Level One",ContactNumber="0213456789",Address="1/5 wolverton st, avondale, auckland"},
                new Customer { Id=1003,FirstName="Danny",LastName="Lan",Email="km@abc.com",Login="danny",Password="password",Role="Level Two",ContactNumber="0213456789",Address="1/5 wolverton st, avondale, auckland"}
            });

            //kernel.Bind<ICustomer>().ToConstant(mock.Object);
            kernel.Bind<ICustomer>().To<CustomerBL>();
            kernel.Bind<IUserRole>().To<RoleBL>();
            kernel.Bind<IProduct>().To<ProductBL>();
            kernel.Bind<ICategory>().To<CategoryBL>();
            kernel.Bind<IPromotion>().To<PromotionBL>();
            kernel.Bind<IFeedback>().To<FeedbackBL>();
            kernel.Bind<IProductAttribute>().To<ProductAttributeBL>();
            kernel.Bind<ITag>().To<TagBL>();
        }
    }
}