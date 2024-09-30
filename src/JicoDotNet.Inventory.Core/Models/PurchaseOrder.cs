using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PurchaseOrder : IPurchaseOrder, IBranch, IVendor, IActivity, IStatus, IHRequest
    {
        public long PurchaseOrderId { get; set; }

        public long PurchaseTypeId { get; set; }
        public long BranchId { get; set; }
        public long VendorId { get; set; }
        public bool IsGstAllowed { get; set; }  // Based on Vendor's IsGSTRegistered on the day of PO

        public DateTime PurchaseOrderDate { get; set; }
        public string PurchaseOrderNumber { get; set; }

        public string AmendmentNumber { get; set; }
        public DateTime? AmendmentDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal PurchaseOrderAmount { get; set; }
        public decimal PurchaseOrderTaxAmount { get; set; }
        public decimal PurchaseOrderTotalAmount { get; set; }

        public string TandC { get; set; }
        public string Remarks { get; set; }

        // IBranch
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        // IVendor
        public long VendorTypeId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string GSTStateCode { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string Mobile { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }

        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        #region PO Grid
        /// <summary>
        /// null - Not received, false - partially received, true - full received
        /// It is to populate the grid of PO List
        /// </summary>
        public bool? GoodsReceivedStatus { get; set; }
        /// <summary>
        /// null - Not billed, false - partially billed, true - full billed
        /// It is to populate the grid of PO List
        /// </summary>
        public bool? BilledStatus { get; set; }
        #endregion

        /// <summary>
        /// if Direct Received, PurchaseOrderNumber is null but GRNNumber will be there. here PO & GRN 1:1 relation
        /// </summary>
        public string GRNNumber { get; set; }

    }
}
