using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Vendor : VendorType, IVendor, IDtoHeader
    {
        public long VendorId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string GSTStateCode { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
