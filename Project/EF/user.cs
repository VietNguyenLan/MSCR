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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.menus = new HashSet<menu>();
            this.orders = new HashSet<order>();
            this.orders1 = new HashSet<order>();
            this.transactions = new HashSet<transaction>();
        }
    
        public string id { get; set; }
        [DisplayName("User Name : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string username { get; set; }

        [DisplayName("Password : ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This Field is Required")]
        public string password { get; set; }
        public string LoginErrorMsg { get; set; }
        public int role { get; set; }

        [DisplayName("Name : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string name { get; set; }

        [DisplayName("Address : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string address { get; set; }

        [DisplayName("Phone Number : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string phone_num { get; set; }

        [DisplayName("Email : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string email { get; set; }


        public string avt_img { get; set; }
        public bool is_active { get; set; }
    
        public virtual balance balance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menu> menus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders1 { get; set; }
        public virtual otp_table otp_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transactions { get; set; }
    }
}
