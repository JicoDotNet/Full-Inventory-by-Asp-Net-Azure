using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class PaymentOut : IPaymentOut, IVendor, IActivity, IStatus, IHRequest
    {
        public long PaymentOutId { get; set; }
        public long VendorId { get; set; }
        public long VendorBankId { get; set; }

        public bool IsTDSApplicable { get; set; }
        public decimal? TDSPercentage { get; set; }
        public decimal? TDSAmount { get; set; }
        public decimal PayAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public short PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public string Remarks { get; set; }

        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeIFSC { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }

        public long VendorTypeId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string GSTStateCode { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<PaymentOutDetail> PaymentOutDetails { get; set; }
    }
}
