using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class CateringViewModel
    {
        public int ID_CATERING { get; set; }
        [Required]
        public int? ID_CLIENT { get; set; }
        [Required]

        public int? ID_MENU{ get; set; }
        public IList<CLIENT> client_id { get; set; }
        [Required]

        public int? AMOUNT_PEOPLE { get; set; }
        [Required]

        public string ADDRESS { get; set; }
        [Required]

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]

        public string ARRIVAL_TIME { get; set; }
        [Required]

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string DEPARTURE_TIME { get; set; }
        public IList<MENU> menuID { get; set; }
        [Required]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EVENT_DATE { get; set; }
        [Required]

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string EVENT_TIME { get; set; }
        [UIHint("BooleanLabel")]
        public bool HAS_PARKING { get; set; }
        [DataType(DataType.MultilineText)]
        public string NOTES { get; set; }

    }

    public class TempOrderViewModel
    {
        public IEnumerable<CATERING_INSUMO> listaInsumos { get; set; }
        public IEnumerable<CATERING_PRODUCTO> listaProductos{ get; set; }
        public IEnumerable<CATERING_MENU> listaMenu{ get; set; }

        public double totalPackageCost { get; set; }
        public double? costoMenu { get; set; }
        public double? costoProducto { get; set; }
        public double? costoSupplie { get; set; }

    }
}

