using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PurchaseReturnDetail : GoodsReceiveNoteDetail, IPurchaseReturnDetail
    {
        public long PurchaseReturnDetailId { get; set; }
        public string PurchaseReturnNumber { get; set; }
        public decimal ReturnedQuantity { get; set; }
    }
}