using JicoDotNet.Inventory.Core.Entities;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Bill : IBill
    {
        public long BillId { get; set; }
        public long PurchaseOrderId { get; set; }
        public long BillTypeId { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime? BillDueDate { get; set; }
        public string BillNumber { get; set; }
        public bool IsFullBilled { get; set; }

        public long VendorId { get; set; }
        public bool IsGstApplicable { get; set; }
        public string GSTNumber { get; set; }
        public string GSTStateCode { get; set; }
        public short GSTType { get; set; }

        public decimal BilledAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public string VendorDONumber { get; set; }
        public string VendorInvoiceNumber { get; set; }
        public DateTime? VendorInvoiceDate { get; set; }
        public string Remarks { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }

        public long VendorTypeId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string PANNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<BillDetail> BillDetails { get; set; }

        // Bill Grid Show (spGetBill - LIST)
        public string PurchaseOrderNumber { get; set; }
        public string GRNNumber { get; set; }

        /// <summary>
        /// Previous Paid Amount, for payment
        /// </summary>
        public decimal PaidAmount { get; set; }

        public bool? PaymentStatus { get; set; }
    }
}
