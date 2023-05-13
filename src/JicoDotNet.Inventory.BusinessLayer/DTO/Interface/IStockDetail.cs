using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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
