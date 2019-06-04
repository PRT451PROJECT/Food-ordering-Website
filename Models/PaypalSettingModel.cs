using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Enum;

namespace BookingTable.Entities.Models
{
    public class PaypalSettingModel
    {
        public string PaypalEmail { get; set; }

        public string PDTToken { get; set; }

        public bool Live { get; set; }
        //esle sandbox
        public string Mode
        {
            get { return Live ? PaypalSettingEnum.Live.ToString() : PaypalSettingEnum.Sandbox.ToString(); }
        }
    }
}
