//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.order_detail = new HashSet<order_detail>();
            this.transactions = new HashSet<transaction>();
        }
    
        public string id { get; set; }
        public string userID { get; set; }
        public System.DateTime create_time { get; set; }
        public System.DateTime take_date { get; set; }
        public int take_time { get; set; }
        public Nullable<System.DateTime> actual_time { get; set; }
        public string staffID { get; set; }
        public bool is_cancle { get; set; }
        public double total_price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_detail> order_detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transactions { get; set; }
        public virtual user user { get; set; }
        public virtual service_time service_time { get; set; }
        public virtual user user1 { get; set; }
    }
}
