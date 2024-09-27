using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IStockDetail : IProductAttribute
    {
        long StockDetailId { get; set; }
        long ProductId { get; set; }
        long WareHouseId { get; set; }

        decimal Stock { get; set; }

        string Remarks { get; set; }
        DateTime GRNDate { get; set; }
    }
}
