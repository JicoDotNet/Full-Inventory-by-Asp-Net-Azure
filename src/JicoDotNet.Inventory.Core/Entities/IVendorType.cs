using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IVendorType : IGenericDescription
    {
        long VendorTypeId { get; set; }
        string VendorTypeName { get; set; }
    }
}
