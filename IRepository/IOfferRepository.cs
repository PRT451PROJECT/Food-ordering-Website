using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface IOfferRepository
    {
        Offers Find(int id);

        List<Offers> GetOffers();

        //bool AddOfferDetais(OfferDetaisl entity)      

        //bool DeleteOfferDetailsById(int id);

        bool Save(Offers entity);

        //bool Delete(Order entity);
    }
}
