using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.Core.Report
{
    public class ResponseProductSalesResult : Product
    {
        public decimal TotalPrice { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalCGST { get; set; }
        public decimal TotalSGST { get; set; }
        public decimal TotalIGST { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalInvoicedAmount { get; set; }

    }
}
