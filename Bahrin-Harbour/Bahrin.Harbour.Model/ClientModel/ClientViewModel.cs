using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bahrin.Harbour.Model.ClientModel
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Client name cannot exceed 100 characters.")]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Required]
        [Range(100000, 999999, ErrorMessage = "ClientId must be a 6-digit number.")]
        [Display(Name = "Client ID")]
        public int ClientId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Postcode cannot exceed 10 characters.")]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Properties must be a positive number.")]
        [Display(Name = "Number of Properties")]
        public int Properties { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Type Of Property cannot exceed 100 characters.")]
        [Display(Name = "Type of Property")]
        public string TypeOfProperty { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Property Location cannot exceed 100 characters.")]
        [Display(Name = "Property Location")]
        public string PropertyLocation { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Property Price must be a positive value.")]
        [Display(Name = "Property Price")]
        public decimal PropertyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Availed Discount must be a positive value.")]
        [Display(Name = "Availed Discount")]
        public decimal AvailedDiscount { get; set; }

        [StringLength(100, ErrorMessage = "Street cannot exceed 100 characters.")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Save Property")]
        public bool SaveProperty { get; set; }

        [Required]
        [Display(Name = "Last Visit")]
        public DateTime LastVisit { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        [Display(Name = "Status")]
        public bool Status { get; set; }
        public string ClientProfileImageLink { get; set; }
        public string ClientQrCodeImageLink { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
