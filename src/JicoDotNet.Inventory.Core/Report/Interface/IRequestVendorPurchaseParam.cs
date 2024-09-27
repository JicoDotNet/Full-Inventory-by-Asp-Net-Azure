using JicoDotNet.Inventory.Core.Common;

namespace JicoDotNet.Inventory.Core.Report.Interface
{
    public interface IRequestVendorPurchaseParam
    {
        long VendorId { get; set; }
        long VendorTypeId { get; set; }
        DateRange SearchDate { get; set; }
    }
}
