using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingTable.Entities.Models
{
    public class MessageModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }
        
        public bool ClosePopup { get; set; }

        

    }
}