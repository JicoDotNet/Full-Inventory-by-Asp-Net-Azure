using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class PurchaseReturnDetailType : IPurchaseReturnDetailType
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