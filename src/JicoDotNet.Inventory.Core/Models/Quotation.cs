using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class Quotation : IQuotation, ICustomer, IActivity, IStatus, IHRequest
    {
        public long QuotationId { get; set; }

        public long CustomerId { get; set; }
        public bool IsGstAllowed { get; set; }
        public DateTime QuotationDate { get; set; }
        public string QuotationNumber { get; set; }

        public decimal QuotationAmount { get; set; }
        public decimal QuotationTaxAmount { get; set; }
        public decimal QuotationTotalAmount { get; set; }

        public string TandC { get; set; }
        public string Remarks { get; set; }

        // ICustomer
        public long CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string GSTStateCode { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string Mobile { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }


        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }

        public List<QuotationDetail> QuotationDetails { get; set; }

        #region QO Grid
        public bool SOGenerated { get; set; }
        #endregion
    }
}