using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class PurchaseReturn : GoodsReceiveNote, IPurchaseReturn
    {
        public long PurchaseReturnId { get; set; }
        public string PurchaseReturnNumber { get; set; }
        public DateTime PurchaseReturnDate { get; set; }
        public bool IsFullReturned { get; set; }
        public string Reason { get; set; }

        public List<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
    }
}