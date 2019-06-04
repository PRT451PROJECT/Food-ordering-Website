using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IOrdersRepository
    {
        Order Find(int id);

        List<Order> GetOrders();
        List<Order> GetDriverAssigned(string id);
        List<Admin> GetDrivers();
        List<Order> GetDailyOrders();

        bool AddOrderDetais(OrderDetail entity);

        List<Order> GetOrdersByTableId(int tableId);

        bool DeleteOrderDetailsById(int id);

        bool Save(Order entity);

        bool Delete(Order entity);
    }
}
