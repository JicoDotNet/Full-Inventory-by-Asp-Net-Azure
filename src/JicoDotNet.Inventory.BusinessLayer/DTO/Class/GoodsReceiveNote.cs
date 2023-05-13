using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class GoodsReceiveNote : IGoodsReceiveNote, IWareHouse, IActivity, IStatus,   IHRequest
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
