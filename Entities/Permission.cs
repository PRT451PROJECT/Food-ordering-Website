namespace BookingTable.Entities.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        [Required]
        [StringLength(32)]
        public string Code { get; set; }

        public bool? Deleted { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateByAdminId { get; set; }

        public virtual Role Role { get; set; }
    }
}
