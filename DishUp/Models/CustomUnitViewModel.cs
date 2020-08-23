using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class CustomUnitViewModel
    {
        public int ID_CUSTOM_UNIT { get; set; }
        public string NAME { get; set; }

        public Nullable<int> ID_PORTIONED_BY { get; set; }
        public Nullable<decimal> QUANTITY { get; set; }
        public Nullable<bool> ACTIVE { get; set; }

    }
}