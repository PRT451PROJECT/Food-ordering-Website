using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Entities.Models
{
    public class SystemSettingModel
    {
        public string TimeDistance { get; set; }

        public string Tax { get; set; }

        public string Discount { get; set; }

        public string BookingLimit { get; set; }

    }
}
