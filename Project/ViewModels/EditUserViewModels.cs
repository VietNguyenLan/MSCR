using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Project.EF;

namespace Project.ViewModels
{
    public class EditUserViewModels
    {
        [DisplayName("User Name : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string username { get; set; }
        public string LoginErrorMsg { get; set; }
        [DisplayName("Role : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public int role { get; set; }
        [DisplayName("Name : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string name { get; set; }
        [DisplayName("Address : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string address { get; set; }
        [DisplayName("Phone : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string phone_num { get; set; }
        [DisplayName("Email : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string email { get; set; }
        [DisplayName("Avatar : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string avt_img { get; set; }
       
    }
}