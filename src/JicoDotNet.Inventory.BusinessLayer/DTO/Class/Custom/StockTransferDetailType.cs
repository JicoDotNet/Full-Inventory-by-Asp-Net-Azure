using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class StockTransferDetailType
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public decimal AvailableQuantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public string Description { get; set; }

        public long StockDetailId { get; set; }
        public bool IsPerishable{ get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
