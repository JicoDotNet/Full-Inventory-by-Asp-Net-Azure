using System;

namespace JicoDotNet.Inventory.Core.Custom.Interface
{
    public interface IStockAdjustDetailType
    {
        int Id { get; set; }
        long ProductId { get; set; }

        /// <summary>
        /// Increment : null, Decrement: Data allowed
        /// </summary>
        decimal? AvailableQuantity { get; set; }

        decimal AdjustQuantity { get; set; }

        /// <summary>
        /// Increment : null, Decrement: Data allowed
        /// </summary>
        long? StockDetailId { get; set; }

        /// <summary>
        /// Increment : Data allowed, Decrement: null
        /// </summary>
        DateTime? GRNDate { get; set; }

        /// <summary>
        /// Increment : Data allowed, Decrement: null
        /// </summary>
        bool IsPerishable { get; set; }
        string BatchNo { get; set; }
        DateTime? ExpiryDate { get; set; }

        string Description { get; set; }
    }
}
