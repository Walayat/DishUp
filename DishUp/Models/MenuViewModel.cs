using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class MenuViewModel
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public string menuName { get; set; }
        public IEnumerable<MENU_PRODUCTO> menuProducto { get; set; }
        public IEnumerable<MENU_INSUMO> menuInsumo { get; set; }
    }
    public class MenuCostViewModel
    {
        public decimal? TotalPackage { get; set; }
        public decimal? TotalSupplie { get; set; }
        public decimal? totalProduts { get; set; }
    }
}