namespace BookingTable.Entities.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Setting")]
    public partial class Setting
    {
        [Key]
        [StringLength(16)]
        public string Key { get; set; }

        [Required]
        [StringLength(64)]
        public string Value { get; set; }

        [Required]
        [StringLength(10)]
        public string Type { get; set; }

        public DateTime LastUpdate { get; set; }

        public int UpdateByAdminId { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
