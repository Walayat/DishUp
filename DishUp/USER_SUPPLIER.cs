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
    
    public partial class USER_SUPPLIER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER_SUPPLIER()
        {
            this.USER_SUPPLIER_INSUMO = new HashSet<USER_SUPPLIER_INSUMO>();
            this.USER_SUPPLIER_INSUMO1 = new HashSet<USER_SUPPLIER_INSUMO>();
        }
    
        public int ID_USER_SUPPLIER { get; set; }
        public string ID_USER { get; set; }
        public Nullable<int> ID_SUPPLIER { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string ABN_NUMBER { get; set; }
        public string ACN_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public string OPEN_HOURS { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_SUPPLIER_INSUMO> USER_SUPPLIER_INSUMO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_SUPPLIER_INSUMO> USER_SUPPLIER_INSUMO1 { get; set; }
    }
}
