using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class StockModels
    {
        public List<WareHouse> _wareHouses { get; set; }
        public IList<Product> _products { get; set; }

        public List<Stock> _stocks { get; set; }
        public Stock _stock { get; set; }

        public StockAdjust _stockAdjust { get; set; }
        public long _productId { get; set; }

        public IProduct _selectedProduct { get; set; }
        public List<StockAdjustReason> _adjustReasons { get; set; }


        // Adjustment Param to manage partial view for stock increment or decrement
        public int ProductId { get; set; }
        public int WareHouseId { get; set; }
        public bool IsStockIncrease { get; set; }
    }
}