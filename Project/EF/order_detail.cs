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
    
    public partial class order_detail
    {
        public int id { get; set; }
        public int orderID { get; set; }
        public int productID { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double total_price { get; set; }
        public double total { get; set; }
        public virtual order order { get; set; }
        public virtual product product { get; set; }
    }
}
