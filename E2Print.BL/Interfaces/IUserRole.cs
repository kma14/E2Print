using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Interfaces
{
    public interface IUserRole
    {
        List<UserRole> GetAll();
        UserRole GetByName(string roleName);
        UserRole Create(UserRole userRole);
        Result Update(UserRole userRole);
        Result Delete(int id);
    }
}
