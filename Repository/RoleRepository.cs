using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Business.IRepository;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookingTableEntities _entities;

        public RoleRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public Role Find(int id)
        {
            return _entities.Roles.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }
        public List<Role> GetRoles()
        {
            return _entities.Roles.Where(x=>x.Deleted != true).ToList();
        }

        //SET
        public bool Save(Role entity)
        {
            try
            {
                _entities.Roles.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Role entity)
        {
            try
            {
                entity.Deleted = true;

                foreach (var entityPermission in entity.Permissions)
                {
                    entityPermission.Deleted = true;
                }

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
