using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Entities.Models
{

    public class FoodModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public bool? Active { get; set; }

        public bool? Deleted { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateByAdminId { get; set; }

        public string Category { get; set; }

    }
  
}
