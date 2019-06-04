using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingTable.Entities.Models
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool Remember { get; set; }

        public string RedirectToUrl { get; set; }
    }
}