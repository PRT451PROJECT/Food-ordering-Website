using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Business.IRepository;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.Repository
{
    public class PermissionRepository :IPermissionRepository
    {
        private readonly BookingTableEntities _entities;

        public PermissionRepository()
        {
            _entities = new BookingTableEntities();
        }

        //SET
        public bool DeleteByRoleId(int id)
        {
            try
            {
                var permissions = _entities.Permissions.Where(x => x.RoleId == id).ToList();

                _entities.Permissions.RemoveRange(permissions);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Save(Permission entity)
        {
            try
            {
                _entities.Permissions.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
