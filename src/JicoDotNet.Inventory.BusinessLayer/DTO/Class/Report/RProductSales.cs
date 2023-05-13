using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class RProductSales : Product
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
