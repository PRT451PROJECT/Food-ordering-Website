using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Entities.Models
{
    public class SearchModel
    {
        public int FloorId { get; set; }

        public int TypeId { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
    }
}
