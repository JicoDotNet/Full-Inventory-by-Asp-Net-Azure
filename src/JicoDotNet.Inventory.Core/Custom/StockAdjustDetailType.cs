using JicoDotNet.Inventory.Core.Custom.Interface;
using System;

namespace JicoDotNet.Inventory.Core.Custom
{
    public class StockAdjustDetailType : IStockAdjustDetailType
    {
        public int Id { get; set; }
        public long ProductId { get; set; }

        /// <summary>
        /// Increment : null, Decrement: Data allowed
        /// </summary>
        public decimal? AvailableQuantity { get; set; }

        public decimal AdjustQuantity { get; set; }

        /// <summary>
        /// Increment : null, Decrement: Data allowed
        /// </summary>
        public long? StockDetailId { get; set; }

        /// <summary>
        /// Increment : Data allowed, Decrement: null
        /// </summary>
        public DateTime? GRNDate { get; set; }

        /// <summary>
        /// Increment : Data allowed, Decrement: null
        /// </summary>
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string Description { get; set; }
    }
}
