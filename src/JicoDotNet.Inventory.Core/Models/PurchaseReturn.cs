using System;
using System.Collections.Generic;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
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