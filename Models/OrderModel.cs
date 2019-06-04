using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Entities.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string TableString { get; set; }
        public string TimeString { get; set; }
        public string Food { get; set; }
        public List<Food> FoodList { get; set; }
        public List<OrderDetail> Orderdetails { get; set; }
    }
}
