using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class PurchaseReturnDetail : GoodsReceiveNoteDetail, IPurchaseReturnDetail
    {
        public long PurchaseReturnDetailId { get; set; }
        public string PurchaseReturnNumber { get; set; }
        public decimal ReturnedQuantity { get; set; }
    }
}