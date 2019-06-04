namespace BookingTable.Entities.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int OrdersId { get; set; }

        public int TableId { get; set; }

        public int? FoodId { get; set; }

        public int CreatorId { get; set; }

        public decimal? FoodPrice { get; set; }

        public string FoodName { get; set; }

        public int? Quantity { get; set; }

        public decimal? Subtotal { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? OrderTime { get; set; }

        public bool? Completed { get; set; }

        public DateTime? LastUpdate { get; set; }

        public int? UpdateByAdminId { get; set; }

        public virtual Food Food { get; set; }

        public virtual Order Order { get; set; }

        public virtual Table Table { get; set; }
    }
}
