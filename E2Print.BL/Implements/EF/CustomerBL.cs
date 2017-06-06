using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.BL.Interfaces;
using E2Print.DAL;
using E2Print.Domain.Entities;

namespace E2Print.BL.Implements.EF
{
    public class CustomerBL:ICustomer
    {
        E2printEntities e2PrintEntities = new E2printEntities();
        
        public IEnumerable<Domain.Entities.Customer> GetAll()
        {
            return ModelMapping.MapCustomer(e2PrintEntities.Customers);
        }

        public IEnumerable<Domain.Entities.Customer> GetById(string customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Entities.Customer> GetByName(string customerName)
        {
            throw new NotImplementedException();
        }

        public Domain.Entities.Customer GetByLogin(string customerLogin)
        {
            return ModelMapping.MapCustomer(e2PrintEntities.Customers.Where(c=>c.Login.Equals(customerLogin)).FirstOrDefault());
        }
        public IEnumerable<Domain.Entities.Customer> GetByRole(string roleName)
        {
            IEnumerable<DAL.Customer> customers = e2PrintEntities.Customers.Where(c=>c.Role.Contains(roleName));
            return ModelMapping.MapCustomer(customers);
        }

        public Domain.Entities.Result Authenticate(string login, string pwd)
        {
            Result result = new Result();
            result.Succeeded = true;

            int matchResult = e2PrintEntities.Customers.Where(c => (c.Login.Equals(login) && c.Password.Equals(pwd))).Count();
            if (matchResult == 0)
            {
                result.Succeeded = false;
                result.Message = "Invalid user name of password";
            }
            return result;
        }

        public Domain.Entities.Result Create(string login, string pwd, string firstName, string lastName, string contactNumber, string address, string email, string company = "",string role="")
        {
            Result result = new Result();
            result.Succeeded = true;

            E2Print.DAL.Customer newCustomer = new DAL.Customer();
            newCustomer.Address = address;
            newCustomer.Login = login;
            newCustomer.Password = pwd;
            newCustomer.FirstName = firstName;
            newCustomer.LastName = lastName;
            newCustomer.ContactNumber = contactNumber;
            newCustomer.Email = email;
            newCustomer.Company = company;
            newCustomer.Role = string.IsNullOrEmpty(role)?Application.DefaultUserRole:role;
            try
            {
                e2PrintEntities.Customers.Add(newCustomer);
                e2PrintEntities.SaveChanges();
                result.Message = newCustomer.Id.ToString();
            }catch(Exception ex){
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Domain.Entities.Result Update(string customerId, string pwd, string firstName, string lastName, string contactNumber, string Address, string email, string company = "")
        {
            throw new NotImplementedException();
        }
        public Result Update(Domain.Entities.Customer customer)
        {
            Result result = new Result();
            result.Succeeded = true;

            try
            {
                DAL.Customer dalCustomer = e2PrintEntities.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();
                dalCustomer.FirstName = customer.FirstName;
                dalCustomer.LastName = customer.LastName;
                dalCustomer.Email = customer.Email;
                dalCustomer.Role = customer.Role;
                dalCustomer.Company = customer.Company;
                dalCustomer.ContactNumber = customer.ContactNumber;
                dalCustomer.Address = customer.Address;

                e2PrintEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
             return result;
        }

        public Domain.Entities.Result Delete(string customerId)
        {
            throw new NotImplementedException();
        }

        public double GetDiscount(string customerId)
        {
            throw new NotImplementedException();
        }


        public Result AddToRole(string roleName, string customerId)
        {
            Result result = new Result();
            result.Succeeded = true;

            DAL.Customer customer = e2PrintEntities.Customers.Where(c => c.Id.Equals(customerId)).FirstOrDefault();
            customer.Role  = roleName;
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

        public Result RemoveFromRole(string customerId)
        {
            Result result = new Result();
            result.Succeeded = true;

            DAL.Customer customer = e2PrintEntities.Customers.Where(c => c.Id.Equals(customerId)).FirstOrDefault();
            customer.Role = "";
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
    }
}
