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
    
    public partial class CATEGORY_SUPPLIE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORY_SUPPLIE()
        {
            this.INSUMOes = new HashSet<INSUMO>();
            this.SUPPLIER_INSUMO = new HashSet<SUPPLIER_INSUMO>();
            this.USER_SUPPLIER_INSUMO = new HashSet<USER_SUPPLIER_INSUMO>();
        }
    
        public int ID_CATEGORY_SUPPLIE { get; set; }
        public string NAME { get; set; }
        public bool ACTIVE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSUMO> INSUMOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPPLIER_INSUMO> SUPPLIER_INSUMO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_SUPPLIER_INSUMO> USER_SUPPLIER_INSUMO { get; set; }
    }
}
