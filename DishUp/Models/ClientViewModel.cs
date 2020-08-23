using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class ClientViewModel
    {
        public int ID_CLIENT { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string HOME_NUMBER { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string EMAIL { get; set; }
    }
}