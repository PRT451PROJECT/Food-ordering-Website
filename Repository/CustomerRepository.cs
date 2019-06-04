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
    public class CustomerRepository :ICustomerRepository
    {
        private readonly BookingTableEntities _entities;

        public CustomerRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public Customer Find(int id)
        {
            return _entities.Customers.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }
        public Customer Login(string username, string password)
        {
            var entity = _entities.Customers.FirstOrDefault(
                    x => x.Username.ToUpper() == username.ToUpper() &&
                    x.Active == true &&
                    x.Deleted != true);

            if (entity != null && password == entity.Password)
            {
                return entity;
            }

            return null;
        }
        public Customer GetActivation(System.Guid activationCode)
        {
            var entity = _entities.Customers.FirstOrDefault(
                    x => x.ActivationCode== activationCode &&
                    x.Active == true &&
                    x.Deleted != true);

            if (entity != null)
            {
                return entity;
            }

            return null;
        }
        public List<Customer> GetCustomers()
        {
            return _entities.Customers.Where(x => x.Deleted != true).ToList();
        }

        //SET
        public bool Save(Customer entity)
        {
            try
            {
                _entities.Customers.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsValid(Customer entity)
        {
            return !_entities.Customers.Any(x => x.Username.ToUpper() == entity.Username.ToUpper() && x.Id != entity.Id && x.Deleted != true);
        }
    }
}
