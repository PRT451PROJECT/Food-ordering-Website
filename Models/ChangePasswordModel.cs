using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Entities.Models
{
    public class ChangePasswordModel
    {
        public string Old { get; set; }

        public string New { get; set; }

        public string Confirm { get; set; }
    }
}
