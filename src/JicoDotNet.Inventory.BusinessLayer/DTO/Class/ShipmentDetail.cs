using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class ShipmentDetail : IShipmentDetail, IActivity, IStatus,   IHRequest
    {
        public long ShipmentDetailId { get; set; }
        public long ShipmentId { get; set; }
        public string ShipmentNumber { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long ProductId { get; set; }
        public decimal ShippedQuantity { get; set; }

        public long StockDetailId { get; set; }

        public bool IsPerishable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }

        public string Description { get; set; }

         

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
