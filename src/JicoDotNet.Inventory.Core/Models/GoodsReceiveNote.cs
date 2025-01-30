using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class GoodsReceiveNote : IGoodsReceiveNote, IWareHouse, IActivity, IStatus, IHttpRequest
    {
        public long GRNId { get; set; }

        public long WareHouseId { get; set; }
        public string GRNNumber { get; set; }
        public DateTime GRNDate { get; set; }

        public bool IsDirect { get; set; }
        public bool IsFullReceived { get; set; }

        public long PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }

        public string VendorDONumber { get; set; }
        public string VendorInvoiceNumber { get; set; }
        public DateTime? VendorInvoiceDate { get; set; }
        public string Remarks { get; set; }

        // IWareHouse
        public long BranchId { get; set; }
        public string WareHouseName { get; set; }
        public bool IsRetailCounter { get; set; }
        public string Description { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }

        public List<GoodsReceiveNoteDetail> GoodsReceiveNoteDetails { get; set; }

        /// <summary>
        /// null - Not billed, false - partially received, true - full billed :: only gor Direct GRN
        /// It is to populate the grid of PO List
        /// </summary>
        public bool? BilledStatus { get; set; }
    }
}
