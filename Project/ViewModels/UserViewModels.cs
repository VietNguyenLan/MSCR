using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.ViewModels
{
    public class UserViewModels
    {
        [DisplayName("User Name : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string username { get; set; }

        [DisplayName("Password : ")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("Remember Me ?")]
        public bool remember { get; set; }

        public string LoginErrorMsg { get; set; }

    }
}