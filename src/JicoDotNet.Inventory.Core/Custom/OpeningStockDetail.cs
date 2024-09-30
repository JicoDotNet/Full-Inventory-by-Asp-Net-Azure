using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class OpeningStockDetail : IOpeningStockDetail
    {
        public int Id { get; set; }
        public long WareHouseId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? GRNDate { get; set; }
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Description { get; set; }
    }
}
