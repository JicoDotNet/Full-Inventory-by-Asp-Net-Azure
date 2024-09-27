using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class GRNDetailType : IGRNDetailType
    {
        public int Id { get; set; }
        public long PurchaseOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string Description { get; set; }
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
