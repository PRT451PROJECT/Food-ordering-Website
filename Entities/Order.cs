namespace BookingTable.Entities.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public int CreatorId { get; set; }

        public int? PayeeId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Tax { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ?OrderTime { get; set; }

        public decimal? DepositPrice { get; set; }

        public DateTime? PaymentTime { get; set; }

        public bool? Completed { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateByAdminId { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Customer Customer { get; set; }

        public int? RoleID  { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
