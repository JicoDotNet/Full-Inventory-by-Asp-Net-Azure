using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IStockTransferDetail : IGenericDescription
    {
        long StockTransferDetailId { get; set; }
        long StockTransferId { get; set; }
        string StockTransferNumber { get; set; }
        long ProductId { get; set; }
        decimal AvailableQuantity { get; set; }
        decimal TransferQuantity { get; set; }
    }
}
