using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IVendor : IGstEntity
    {
        long VendorId { get; set; }
        long VendorTypeId { get; set; }
        string CompanyName { get; set; }
        string CompanyType { get; set; }
        string StateCode { get; set; }
        string PANNumber { get; set; }
        string ContactPerson { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
    }
}
