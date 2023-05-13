using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class RCustomerSales : Customer
    {
        public long TotalInvoiceCount { get; set; }
        public decimal SumInvoicedAmount { get; set; }
        public decimal SumTaxAmount { get; set; }
        public decimal SumTotalAmount { get; set; }
    }
}
