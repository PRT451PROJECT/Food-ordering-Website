using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Business.IRepository;
using BookingTable.Entities.Entities;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using BookingTable.Entities.Models;

namespace BookingTable.Business.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly BookingTableEntities _entities;

        public FeedBackRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public FeedBack Find(int id)
        {
            return _entities.FeedBack.FirstOrDefault(x => x.CommentId == id );

        }
        //GET
        public List<FeedBack> SearchFeedBackName(string FoodName)
        {  
            return _entities.FeedBack.Where(c => c.Rating != null).ToList();
        }
        public List<FeedBack> GetFeedBack()
        {
            return _entities.FeedBack.Where(x => x.CommentId  >0).ToList();
        }
        ////GET
        //public List<Food> GetOffers()
        //{
        //    return _entities.Foods.Where(c => c.Offer_Id != null && c.Offer_Id != 2).ToList();
        //}

        //SET
        public bool Save(FeedBack entity)
        {
            try
            {
                _entities.FeedBack.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public List<Food> GetValidFoods()
        //{
        //    try
        //    {
        //        return _entities.Foods.Where(x => x.Quantity > 0 && x.Active != false && x.Deleted != true).ToList();
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}
    }
}
