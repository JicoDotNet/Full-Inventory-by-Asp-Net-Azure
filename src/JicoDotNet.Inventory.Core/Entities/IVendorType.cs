namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IVendorType
    {
        long VendorTypeId { get; set; }
        string VendorTypeName { get; set; }
        string Description { get; set; }
    }
}
