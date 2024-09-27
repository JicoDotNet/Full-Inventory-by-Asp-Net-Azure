using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class StockTransferDetailType : IStockTransferDetailType
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public decimal AvailableQuantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public string Description { get; set; }

        public long StockDetailId { get; set; }
        public bool IsPerishable{ get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
