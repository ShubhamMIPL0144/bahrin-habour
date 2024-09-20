using Microsoft.AspNetCore.Identity;

namespace Bahrin.Harbour.Data.DBCollections
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string Role { get; set; }
        public string? BranchAssigned { get; set; }
        public string? OutletAssigned { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePathfolder { get; set; }
        public string? CreatedBy { get; set; }
        public string? DeviceToken { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }

    }
}
