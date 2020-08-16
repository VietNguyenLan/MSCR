using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.EF;

namespace Project.Models
{
    public class IngredientDetail
    {
        public ingredient Ingredient { get; set; }
        public int amount { get; set; }
    }
}