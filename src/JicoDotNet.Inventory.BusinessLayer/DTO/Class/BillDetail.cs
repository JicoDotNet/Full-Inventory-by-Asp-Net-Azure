using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class BillDetail : IBillDetail, IActivity, IStatus,   IHRequest
    {
        public long BillDetailId { get; set; }
        
        public long BillId { get; set; }
        public string BillNumber { get; set; }
        public long PurchaseOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string HSNSAC { get; set; }
        public decimal Price { get; set; }
        public decimal BilledQuantity { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal Total { get; set; }

        public string Description { get; set; }

         
         
        public bool IsActive { get; set; }
        public DateTime TransactionDate { get; set; }
        public string RequestId { get; set; }
         
    }
}
