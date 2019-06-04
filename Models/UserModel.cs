using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookingTable.Entities.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string ImageName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string IdentityCard { get; set; }

        public bool? Active { get; set; }       

        public string RedirectToUrl { get; set; }
    }
}
