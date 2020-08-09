using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2Print.Domain.Entities;

namespace E2Print.BL.Interfaces
{
    public interface ITag
    {
        List<Tag> GetAll();
        Tag GetById(Guid id);
        List<Tag> GetByType(string typeName);
        Tag Create(Tag tag);
        Result Update(Tag tag);
        Result Delete(Guid id);
    }
}
