using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookingTable.Entities;
using BookingTable.Entities.Entities;

namespace BookingTable.Entities.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PermissionString { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}