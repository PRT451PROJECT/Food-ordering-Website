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
    public class AdminRepository : IAdminRepository
    {
        private readonly BookingTableEntities _entities;

        public AdminRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public Admin Find(int id)
        {
            return _entities.Admins.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }
        public Admin Login(string username, string password)
        {
            var entity = _entities.Admins.FirstOrDefault(
                x => x.Username.ToUpper() == username.ToUpper() &&
                     x.Active == true &&
                     x.Deleted != true);

            if (entity != null && password.Equals(entity.Password))
            {
                return entity;
            }
            return null;
        }
        public List<Admin> GetAdmins()
        {
            return _entities.Admins.Where(x => x.Deleted != true).ToList();
        }
        public List<Admin> GetDrivers()
        {
            return _entities.Admins.Where(x => x.Deleted != true && x.RoleId == 1007).ToList();
        }
        public bool IsValid(Admin entity)
        {
            return !_entities.Admins.Any(x => x.Username.ToUpper() == entity.Username.ToUpper() && x.Id != entity.Id && x.Deleted != true);
        }

        //SET
        public bool Save(Admin entity)
        {
            try
            {
                _entities.Admins.AddOrUpdate(entity);

                _entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}
