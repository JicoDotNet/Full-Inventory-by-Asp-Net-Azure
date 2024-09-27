using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Report.Interface;

namespace JicoDotNet.Inventory.Core.Report
{
    public class RequestVendorPurchaseParam : IRequestVendorPurchaseParam
    {
        public long VendorId { get; set; }
        public long VendorTypeId { get; set; }
        public DateRange SearchDate { get; set; }
    }
}
