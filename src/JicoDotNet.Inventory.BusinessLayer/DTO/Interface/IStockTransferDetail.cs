using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IStockTransferDetail
    {
        long StockTransferDetailId { get; set; }
        long StockTransferId { get; set; }
        string StockTransferNumber { get; set; }
        long ProductId { get; set; }
        decimal AvailableQuantity { get; set; }
        decimal TransferQuantity { get; set; }
        string Description { get; set; }
    }
}
