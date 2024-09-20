namespace Bahrin.Harbour.Model.ClientModel
{
    public class Client
    {
        public Guid id { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Postcode { get; set; } 
        public string Address { get; set; } 
        public int Properties { get; set; }
        public string TypeOfProperty { get; set; }  
        public string PropertyLocation { get; set; }
        public decimal PropertyPrice { get; set; } 
        public decimal AvailedDiscount { get; set; }
        public string Street { get; set; }
        public string ClientProfileImageFileName { get; set; }
        public string ImageFolderName { get; set; }
        public string QrImageName { get; set; }
        public bool SaveProperty { get; set; }
        public bool Status { get; set; }
        public DateTime LastVisit { get; set; }
        public DateTime LastModified { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime CreatedBy { get; set; }
    }
}
