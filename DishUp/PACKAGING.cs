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
    
    public partial class PACKAGING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACKAGING()
        {
            this.CAT_INS_PACK = new HashSet<CAT_INS_PACK>();
            this.INSUMO_PACKAGING = new HashSet<INSUMO_PACKAGING>();
        }
    
        public int ID_PACKAGING { get; set; }
        public string IMAGE { get; set; }
        public string NAME { get; set; }
        public Nullable<decimal> UNIT_PRICE { get; set; }
        public Nullable<decimal> WHOLESALE_PRICE { get; set; }
        public int ACTIVE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAT_INS_PACK> CAT_INS_PACK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSUMO_PACKAGING> INSUMO_PACKAGING { get; set; }
    }
}
