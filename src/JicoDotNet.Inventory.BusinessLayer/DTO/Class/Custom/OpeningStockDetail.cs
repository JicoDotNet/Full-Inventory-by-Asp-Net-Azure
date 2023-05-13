using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class OpeningStockDetail
    {
        public int Id { get; set; }
        public long WareHouseId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? GRNDate { get; set; }        
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Description { get; set; }
    }
}
