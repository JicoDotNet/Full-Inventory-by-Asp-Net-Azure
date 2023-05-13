using System;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class GRNDetailType
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
