using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.Core.Report
{
    public class ResponseCustomerSalesResult : Customer
    {
        public long TotalInvoiceCount { get; set; }
        public decimal SumInvoicedAmount { get; set; }
        public decimal SumTaxAmount { get; set; }
        public decimal SumTotalAmount { get; set; }
    }
}
