using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Entities.Models
{
    public class TableModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Seats { get; set; }

        public string TypeName { get; set; }

        public string DepositPrice { get; set; }
        
        public string Status { get; set; }

        public int OrderId { get; set; }

        public DateTime Time { get; set; }

    }
}
