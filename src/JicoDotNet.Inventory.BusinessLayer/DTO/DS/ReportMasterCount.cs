using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class ReportMasterCount
    {
        public long PurchaseOrderMonth { get; set; }
        public decimal PurchaseOrderTotalAmountMonth { get; set; }
        public decimal BilledAmountMonth { get; set; }
        public decimal PayTotalAmountMonth { get; set; }

        public long SalesOrderMonth { get; set; }
        public decimal SalesOrderTotalAmountMonth { get; set; }
        public decimal InvoicedAmountMonth { get; set; }
        public decimal ReceiveTotalAmountMonth { get; set; }

        public decimal TDSPayAmountMonth { get; set; }
        public decimal TDSPaidAmountMonth { get; set; }
        public decimal TDSReceiveAmountMonth { get; set; }
        public decimal TDSReceivedAmountMonth { get; set; }
    }
}
