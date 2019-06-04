using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IPermissionRepository
    {
        bool DeleteByRoleId(int id);

        bool Save(Permission entity);
    }
}
