using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    /// <summary>
    /// This class Only for Model POST from HTML to Controller and Logic.
    /// </summary>
    public class GoodsReceiveNoteDirectDetail : PurchaseOrderDetail, IProductAttribute
    {
        public bool IsPerishable { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
