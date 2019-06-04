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
    public class OfferRepository : IOfferRepository
    {
        private readonly BookingTableEntities _entities;

        public OfferRepository()
        {
            _entities = new BookingTableEntities();
        }

       // GET
        public Offers Find(int id)
        {
            return _entities.Offers.SingleOrDefault(x => x.Id == id);
            
        }
        public List<Offers> GetOffers()
        {
            return _entities.Offers.Where(x => x.Deleted != true && x.Id != 2).ToList();
        }

        ////SET
        //public bool AddOrderDetais(OrderDetail entity)
        //{
        //    try
        //    {
        //        _entities.OrderDetails.Add(entity);
        //        _entities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        //public bool DeleteOrderDetailsById(int id)
        //{
        //    try
        //    {
        //        var entity = _entities.OrderDetails.Find(id);

        //        _entities.OrderDetails.Remove(entity);

        //        _entities.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public bool Save(Offers entity)
        {
            try
            {
                _entities.Offers.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public List<Order> GetOrdersByTableId(int tableId)
        //{
        //    try
        //    {
        //        return _entities.Orders.Where(x => x.OrderDetails.Any(y=>y.TableId == tableId)).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public bool Delete(Order entity)
        //{
        //    try
        //    {
        //        _entities.Orders.Remove(entity);

        //        _entities.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}

