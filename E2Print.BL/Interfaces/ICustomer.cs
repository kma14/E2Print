using E2Print.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2Print.BL.Interfaces
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetById(string customerId);
        IEnumerable<Customer> GetByName(string customerName);
        Customer GetByLogin(string customerLogin);
        IEnumerable<Customer> GetByRole(string roleName);

        Result Authenticate(string login, string pwd);
        Result Create(string login, string pwd, string firstName, string lastName, string contactNumber,string Address,string email,string company = "",string role="");
        Result Update(Customer customer);
        Result Update(string customerId, string pwd, string firstName, string lastName, string contactNumber, string Address, string email, string company = "");
        Result Delete(string customerId);
        Result AddToRole(string roleName, string customerId);
        Result RemoveFromRole(string customerId); //need to modify
        double GetDiscount(string customerId);
    }
}
