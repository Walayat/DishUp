using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class ClientHistoryViewModel
    {
        public int? ID_CATERING { get; set; }

        public string clientName{ get; set; }
        public string address{ get; set; }
        public string eventDate { get; set; }
    }
}