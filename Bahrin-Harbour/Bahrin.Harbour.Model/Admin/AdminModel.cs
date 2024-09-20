using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace PuthaganModel.Admin
{
   /* public class AdminModel
    {
        public Guid _id { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        public DateTime dateofBirth { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string email { get; set; }

        [Required(ErrorMessage = "The phone number is required")]
        [Display(Name = "Phone number")]
        public string phoneNumber { get; set; }
        public string image { get; set; }
        public string deviceToken { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public Guid createdBy { get; set; }
        public Guid modifiedBy { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string password { get; set; }
        public string passCode { get; set; }

        //[Required(ErrorMessage = "Please choose profile image")]
        //[Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public bool viewOnly { get; set; }
        public string OldEmail { get; set; }
        public bool isSuperAdmin { get; set; }
        public int localTimeZoneOffset { get; set; }
    }*/
}
