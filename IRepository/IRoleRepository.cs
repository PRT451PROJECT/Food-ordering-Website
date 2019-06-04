using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IRoleRepository
    {
        Role Find(int id);
        List<Role> GetRoles();
        bool Save(Role entity);
        bool Delete(Role entity);
    }
}
