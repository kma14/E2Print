using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.DAL;
using E2Print.BL.Interfaces;
using E2Print.Domain.Entities;
using System.Data.Entity;

namespace E2Print.BL.Implements.EF
{
    public class RoleBL:IUserRole
    {
        E2printEntities e2PrintEntities = new E2printEntities();

        public List<E2Print.Domain.Entities.UserRole> GetAll()
        {
            List<E2Print.Domain.Entities.UserRole> roles = new List<E2Print.Domain.Entities.UserRole>();

            foreach (var dalRole in e2PrintEntities.Roles)
            {
                E2Print.Domain.Entities.UserRole role = ModelMapping.MapRole(dalRole);
                roles.Add(role);
            }
            return roles;
        }

        public UserRole Create(UserRole userRole)
        {
            Role role = new Role { RoleName = userRole.RoleName, Discount = userRole.Discount };
            e2PrintEntities.Roles.Add(role);
            e2PrintEntities.SaveChanges();
            userRole = ModelMapping.MapRole(role);
            return userRole;
        }

        public Result Update(UserRole userRole)
        {
            Result result = new Result();
            result.Succeeded = true;

            Role role = e2PrintEntities.Roles.Where(c => c.Id == userRole.Id).FirstOrDefault();
            role.RoleName = userRole.RoleName;
            role.Discount = userRole.Discount;
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
        public Result Delete(int id) 
        {
            Result result = new Result();
            result.Succeeded = true;

            Role role = e2PrintEntities.Roles.Where(c => c.Id == id).FirstOrDefault();
            try
            {
                e2PrintEntities.Roles.Remove(role);
                e2PrintEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }


        public UserRole GetByName(string roleName)
        {
            Role role = e2PrintEntities.Roles.Where(c => c.RoleName.Equals(roleName)).FirstOrDefault();
            return ModelMapping.MapRole(role);
        }
    }
}
