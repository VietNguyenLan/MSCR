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
            this.topup_card = new HashSet<topup_card>();
            this.transactions = new HashSet<transaction>();
        }

        public int id { get; set; }
        [DisplayName("User Name : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public string username { get; set; }
        [DisplayName("Password : ")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
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
        [DisplayName("Active : ")]
        [Required(ErrorMessage = "This Field is Required")]
        public bool is_active { get; set; }
        public bool email_verified { get; set; }

        public virtual balance balance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menu> menus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders1 { get; set; }
        public virtual otp_table otp_table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<topup_card> topup_card { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaction> transactions { get; set; }
    }
}
