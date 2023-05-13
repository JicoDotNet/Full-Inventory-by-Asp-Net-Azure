using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class GoodsReceiveNoteDetail : IGoodsReceiveNoteDetail, IActivity, IStatus,   IHRequest
    {
        public long GRNDetailId { get; set; }
        public long GRNId { get; set; }
        public string GRNNumber { get; set; }
        public long PurchaseOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string Description { get; set; }
        
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
