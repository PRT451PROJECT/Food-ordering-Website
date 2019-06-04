namespace BookingTable.Entities.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeedBack()
        {
            //OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Comments { get; set; }

        [Required]
        [StringLength(16)]
        public string Rating { get; set; }

        public DateTime? CommentsDate { get; set; }

        

        
    }
}
