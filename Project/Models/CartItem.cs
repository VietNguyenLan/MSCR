using Project.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class CartItem
    {
        public product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int ServiceTime { get; set; }
    }
}