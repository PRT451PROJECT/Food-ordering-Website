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
    public class FoodRepository : IFoodRepository
    {
        private readonly BookingTableEntities _entities;

        public FoodRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public Food Find(int id)
        {
            return _entities.Foods.FirstOrDefault(x => x.Id == id && x.Deleted != true);

        }
        //GET
        public List<Food> SearchFoodName(string FoodName)
        {  
            return _entities.Foods.Where(c => c.Name != null && c.Name.ToLower().Contains(FoodName.ToLower())).ToList();
        }
        public List<Food> GetFoods()
        {
            return _entities.Foods.Where(x => x.Deleted != true).ToList();
        }
        public List<Food> GetTopFoods()
        {
            return _entities.Foods.Where(c => c.Name != null && c.Name.ToLower().Contains("meat") || c.Name.ToLower().Contains("zeo")).ToList();
        }
        //GET
        public List<Food> GetOffers()
        {
            return _entities.Foods.Where(c => c.Offer_Id > 0 && c.Offer_Id != 2).ToList();
        }

        //SET
        public bool Save(Food entity)
        {
            try
            {
                _entities.Foods.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Food> GetValidFoods()
        {
            try
            {
                return _entities.Foods.Where(x => x.Quantity > 0 && x.Active != false && x.Deleted != true).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<Food> GetPizzaFoods()
        {
            try
            {
                return _entities.Foods.Where(x => x.Quantity > 0 && x.Active != false && x.Deleted != true && x.Category == "Pizza").ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public List<Food> GetFoodSelectList()
        {
            try
            {
                return _entities.Foods.Where(x => x.Quantity > 0 && x.Active != false && x.Deleted != true).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Food> GetDrinksFoods()
        {
            try
            {
                return _entities.Foods.Where(x => x.Quantity > 0 && x.Active != false && x.Deleted != true && x.Category == "Drinks").ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
