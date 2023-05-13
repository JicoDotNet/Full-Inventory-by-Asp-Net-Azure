using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IStockAdjustDetail
    {
        long StockAdjustDetailId { get; set; }

        long StockAdjustId { get; set; }
        string StockAdjustNumber { get; set; }
        bool IsStockIncrease { get; set; }

        long ProductId { get; set; }
        decimal AvailableQuantity { get; set; }
        decimal AdjustQuantity { get; set; }

        bool IsPerishable { get; set; }
        DateTime? ExpiryDate { get; set; }
        string BatchNo { get; set; }
        DateTime? GRNDate { get; set; }

        long StockDetailId { get; set; }       
        
        string Description { get; set; }
    }
}
