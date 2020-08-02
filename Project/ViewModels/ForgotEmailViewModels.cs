using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.EF;

namespace Project.ViewModels
{
    public class ForgotEmailViewModels
    {

        [Display(Name = "Email: ")]
        public string Email { get; set; }

        public string ErrorMgs { get; set; }
    }
}