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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BookingTableEntities _entities;

        public OrdersRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public Order Find(int id)
        {
            return _entities.Orders.SingleOrDefault(x => x.Id == id);
        }
        public List<Order> GetOrders()
        {
            return _entities.Orders.Where(x => x.Completed != true).ToList();
        }
        public List<Order> GetDailyOrders()
        {
            
            return _entities.Orders.Where(x => x.Completed != true).ToList();
        }

        //SET
        public bool AddOrderDetais(OrderDetail entity)
        {
            try
            {
                _entities.OrderDetails.Add(entity);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteOrderDetailsById(int id)
        {
            try
            {
                var entity = _entities.OrderDetails.Find(id);

                _entities.OrderDetails.Remove(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Save(Order entity)
        {
            try
            {
                _entities.Orders.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Order> GetOrdersByTableId(int tableId)
        {
            try
            {
                return _entities.Orders.Where(x => x.OrderDetails.Any(y=>y.TableId == tableId)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Admin> GetDrivers()
        {
            return _entities.Admins.Where(x => x.Deleted != true && x.RoleId == 1007).ToList();
        }
        public List<Order> GetDriverAssigned(string id)
        {
            int Id = Convert.ToInt32(id);
            return _entities.Orders.Where(x => x.Completed != true && x.RoleID==Id ).ToList();
        }
         

        public bool Delete(Order entity)
        {
            try
            {
                _entities.Orders.Remove(entity);

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

