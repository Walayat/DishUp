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
    
    public partial class RECIPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RECIPE()
        {
            this.RECIPE_DETAIL = new HashSet<RECIPE_DETAIL>();
        }
    
        public int ID_RECIPE { get; set; }
        public Nullable<int> ID_CATEGORY { get; set; }
        public Nullable<int> ID_MEDIDA_YIELD { get; set; }
        public Nullable<int> ID_MEDIDA_PORTION { get; set; }
        public Nullable<int> ID_PREP_TIME { get; set; }
        public Nullable<int> ID_COOK_TIME { get; set; }
        public Nullable<int> ID_SHELF_TIME { get; set; }
        public Nullable<int> YIELD { get; set; }
        public Nullable<int> PORTION_SIZE { get; set; }
        public Nullable<decimal> PORTION_NUMBER { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<int> PREP_TIME { get; set; }
        public Nullable<int> COOK_TIME { get; set; }
        public Nullable<int> SHELF_LIFE { get; set; }
        public string IMAGEN { get; set; }
        public string NAME { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
    
        public virtual MEDIDA MEDIDA { get; set; }
        public virtual MEDIDA MEDIDA1 { get; set; }
        public virtual CATEGORY CATEGORY { get; set; }
        public virtual CATEGORY CATEGORY1 { get; set; }
        public virtual CATEGORY CATEGORY2 { get; set; }
        public virtual CATEGORY CATEGORY3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECIPE_DETAIL> RECIPE_DETAIL { get; set; }
    }
}