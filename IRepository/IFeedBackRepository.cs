using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IFeedBackRepository
    {
        FeedBack Find(int id);
        List<FeedBack> SearchFeedBackName(string FoodName);
        List<FeedBack> GetFeedBack();
        bool Save(FeedBack entity);
        
    }
}
