using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IVendorBank : IBankCommon
    {
        long VendorBankId { get; set; }
        long VendorId { get; set; }
    }
}
