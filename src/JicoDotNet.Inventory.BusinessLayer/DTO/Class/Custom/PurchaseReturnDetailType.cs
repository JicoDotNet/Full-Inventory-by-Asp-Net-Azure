using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class PurchaseReturnDetailType
    {
        public int Id { get; set; }
        public long GRNDetailId { get; set; }
        public long PurchaseOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public string Description { get; set; }
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}