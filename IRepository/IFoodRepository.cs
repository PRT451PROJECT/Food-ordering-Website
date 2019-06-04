using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IFoodRepository
    {
        Food Find(int id);
        List<Food> SearchFoodName (string FoodName);
        List<Food> GetDrinksFoods();
        List<Food> GetPizzaFoods();
        List<Food> GetFoodSelectList();
        List<Food> GetFoods();
        List<Food> GetTopFoods();
        bool Save(Food entity);
        List<Food> GetValidFoods();
        List<Food> GetOffers();
    }
}
