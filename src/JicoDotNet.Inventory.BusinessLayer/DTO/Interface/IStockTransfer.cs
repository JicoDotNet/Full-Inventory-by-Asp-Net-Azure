using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IStockTransfer
    {
        long StockTransferId { get; set; }
        string StockTransferNumber { get; set; }
        DateTime StockTransferDate { get; set; }
        long FromWareHouseId { get; set; }
        long ToWareHouseId { get; set; }
        string Remarks { get; set; }
    }
}
