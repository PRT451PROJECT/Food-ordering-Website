using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Entities.Models
{

   
    class OfferModel
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public int OfferAmount { get; set; }
        public DateTime? OfferValidity { get; set; }
        public List<Offers> OfferList { get; set; }        
    }
}
