using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PurchaseOrderDetail : Product, IPurchaseOrderDetail
    {
        public long PurchaseOrderDetailId { get; set; }

        public long PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AmendmentNumber { get; set; }

        public decimal Amount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }

        /// <summary>
        /// GRN & Bill - Partially Received or Billed
        /// </summary>
        public decimal ReceivedQuantity { get; set; }
    }
}
