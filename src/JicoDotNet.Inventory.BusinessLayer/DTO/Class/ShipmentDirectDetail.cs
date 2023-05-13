using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class ShipmentDirectDetail : SalesOrderDetail, IProductAttribute
    {
        public long StockDetailId { get; set; }

        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
    }
}
