using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IAdminRepository
    {
        Admin Find(int id);
        Admin Login(string username, string password);
        List<Admin> GetAdmins();
        List<Admin> GetDrivers();
        bool Save(Admin entity);
        bool IsValid(Admin entity);

    }
}
