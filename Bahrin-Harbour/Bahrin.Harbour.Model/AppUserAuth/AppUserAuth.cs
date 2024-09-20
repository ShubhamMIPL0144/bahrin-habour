using Bahrin.Harbour.Model.AccountModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bahrin.Harbour.Model.AppUserAuth
{
    public class AppUserAuth
    {
    }

    public class SigninResponse
    {
        public AppUserViewModel data { get; set; }
        public StatusModel status { get; set; }
    }


public class AppUserViewModel
    {
        public Guid _id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters.")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "State can't be longer than 50 characters.")]
        public string State { get; set; }

        [StringLength(50, ErrorMessage = "Country can't be longer than 50 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "User status is required.")]
        public bool? IsActive { get; set; }

    
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(256, ErrorMessage = "Email can't be longer than 256 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        public string BranchAssigned { get; set; }
        public string OutletAssigned { get; set; }
        public string ImageLink { get; set; }
        public IFormFile Image { get; set; }
        public string deviceToken { get; set; }
    }
}
