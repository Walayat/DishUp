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
    
    public partial class MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MENU()
        {
            this.CATERINGs = new HashSet<CATERING>();
            this.CATERING_MENU = new HashSet<CATERING_MENU>();
            this.MENU_INSUMO = new HashSet<MENU_INSUMO>();
            this.MENU_PRODUCTO = new HashSet<MENU_PRODUCTO>();
            this.MENU_PRODUCTO1 = new HashSet<MENU_PRODUCTO>();
        }
    
        public int ID_MENU { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
        public Nullable<bool> isDelivery { get; set; }
        public Nullable<decimal> COST { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATERING> CATERINGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CATERING_MENU> CATERING_MENU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MENU_INSUMO> MENU_INSUMO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MENU_PRODUCTO> MENU_PRODUCTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MENU_PRODUCTO> MENU_PRODUCTO1 { get; set; }
    }
}
