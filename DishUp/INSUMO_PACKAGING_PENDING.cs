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
    
    public partial class INSUMO_PACKAGING_PENDING
    {
        public int ID_INS_PACK_PENDING { get; set; }
        public Nullable<int> ID_INSUMO { get; set; }
        public Nullable<decimal> QUANTITY { get; set; }
        public bool ACTIVE { get; set; }
    
        public virtual INSUMO INSUMO { get; set; }
    }
}