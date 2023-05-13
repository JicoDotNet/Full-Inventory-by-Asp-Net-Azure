using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class InvoiceDetail : IInvoiceDetail, IActivity, IStatus,   IHRequest
    {
        public long InvoiceDetailId { get; set; }

        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string HSNSAC { get; set; }
        public decimal Price { get; set; }
        public decimal InvoicedQuantity { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal Total { get; set; }

        public string Description { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
