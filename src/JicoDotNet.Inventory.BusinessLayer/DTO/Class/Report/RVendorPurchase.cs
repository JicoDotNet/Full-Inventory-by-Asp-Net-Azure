using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class RVendorPurchase : Vendor
    {
        public long TotalBillCount { get; set; }
        public decimal SumBilledAmount { get; set; }
        public decimal SumTaxAmount { get; set; }
        public decimal SumTotalAmount { get; set; }
    }
}
