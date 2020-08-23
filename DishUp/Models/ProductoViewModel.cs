using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DishUp.Models
{
    public class ProductoViewModel
    {
        public IEnumerable<PRODUCTO> listaProducto { get; set; }
    }

    public class InsumosViewModel 
    {
        public string nombre { get; set; }
        public string nombreInsumo { get; set; }
        public int? id { get; set; }
        public int? quantity { get; set; }
    }

    public class ProductoInsumoViewModel
    {
        public int ID_PRODUCTO_INSUMO { get; set; }
        public Nullable<int> ID_PRODUCTO { get; set; }
        public Nullable<int> ID_INSUMO { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> ID_MEDIDA { get; set; }

    }

    public class supplieListViewModel
    {
        public IEnumerable<PRODUCTO_INSUMO> listaInsumos { get; set; }
        public decimal? costoTotal { get; set; }
    }
    public class ProfitCalculator
    {
        public int ID_PRODUCTO { get; set; }

        [MinLength(0,ErrorMessage ="Value must be 0 or greater than 0")]
        public decimal profitpercentaje { get; set; }
    }
}