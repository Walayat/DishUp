//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DishUp
{
    using System;
    using System.Collections.Generic;
    
    public partial class RECIPE_DETAIL
    {
        public int ID_RECIPE_DETAIL { get; set; }
        public Nullable<int> ID_USER_SUPPLIER_INSUMO { get; set; }
        public Nullable<int> ID_PORTIONED_BY { get; set; }
        public Nullable<decimal> QUANTITY { get; set; }
        public Nullable<decimal> COST { get; set; }
        public Nullable<decimal> COST_PERCENTAGE { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
        public Nullable<int> ID_RECIPE { get; set; }
    
        public virtual MEDIDA MEDIDA { get; set; }
        public virtual USER_SUPPLIER_INSUMO USER_SUPPLIER_INSUMO { get; set; }
        public virtual RECIPE RECIPE { get; set; }
    }
}
