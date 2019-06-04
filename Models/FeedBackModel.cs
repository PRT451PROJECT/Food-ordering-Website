using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Entities.Models
{

    public class FeedBackModel
    {
        [Key]
        public int CommentId { get; set; }

        public string Comments { get; set; }        

        public string Rating { get; set; }

        public DateTime? CommentsDate { get; set; }
       
    }
  
}
