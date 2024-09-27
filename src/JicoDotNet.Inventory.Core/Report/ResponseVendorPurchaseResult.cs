using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.Core.Report
{
    public class ResponseVendorPurchaseResult : Vendor
    {
        public long TotalBillCount { get; set; }
        public decimal SumBilledAmount { get; set; }
        public decimal SumTaxAmount { get; set; }
        public decimal SumTotalAmount { get; set; }
    }
}
