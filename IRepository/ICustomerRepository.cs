using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface ICustomerRepository
    {
        Customer Find(int id);
        Customer Login(string username, string password);
        Customer GetActivation(System.Guid activationCode);
        List<Customer> GetCustomers();
        bool Save(Customer entity);
        bool IsValid(Customer entity);
    }
}
