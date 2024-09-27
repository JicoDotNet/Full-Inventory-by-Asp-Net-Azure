using System;
using System.Collections.Generic;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    /// <summary>
    /// This class Only for Model POST from HTML to Controller and Logic. 
    /// </summary>
    public class GoodsReceiveNoteDirect : GoodsReceiveNote, IPurchaseOrder
    {         
        public long PurchaseTypeId { get; set; }
        public long VendorId { get; set; }
        public bool IsGstAllowed { get; set; }

        public DateTime PurchaseOrderDate { get; set; }
         
        public string AmendmentNumber { get; set; }
        public DateTime? AmendmentDate { get; set; }
         
        public DateTime? DeliveryDate { get; set; }
         
        public decimal PurchaseOrderAmount { get; set; }
        public decimal PurchaseOrderTaxAmount { get; set; }
        public decimal PurchaseOrderTotalAmount { get; set; }
         
        public string TandC { get; set; }

        // This prop(s) are for GRN Direct it self
        public bool IsBillGenerated { get; set; }
        public bool IsPaid { get; set; }

        public List<GoodsReceiveNoteDirectDetail> GoodsReceiveNoteDirectDetails { get; set; }
    }
}
