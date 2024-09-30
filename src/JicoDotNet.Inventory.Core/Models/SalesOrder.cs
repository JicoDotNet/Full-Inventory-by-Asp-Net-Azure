using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Models
{
    public class SalesOrder : ISalesOrder, IBranch, ICustomer, IActivity, IStatus, IHRequest
    {
        public long SalesOrderId { get; set; }
        public long? QuotationId { get; set; }
        public long SalesTypeId { get; set; }
        public long BranchId { get; set; }
        public long CustomerId { get; set; }
        public bool IsGstAllowed { get; set; }  // Based on Company's IsGSTRegistered on the day of SO
        public DateTime SalesOrderDate { get; set; }
        public string SalesOrderNumber { get; set; }
        public string AmendmentNumber { get; set; }
        public DateTime? AmendmentDate { get; set; }
        public string CustomerPONumber { get; set; }
        public DateTime? CustomerPODate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal SalesOrderAmount { get; set; }
        public decimal SalesOrderTaxAmount { get; set; }
        public decimal SalesOrderTotalAmount { get; set; }
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
        public string CustomerTypeName { get; set; }
        public string Description { get; set; }

        // ICustomer
        public long CustomerTypeId { get; set; }
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

        public List<SalesOrderDetail> SalesOrderDetails { get; set; }

        #region SO Grid
        /// <summary>
        /// null - Not shipped, false - partially shipped, true - full shipped
        /// It is to populate the grid of SO List
        /// </summary>
        public bool? ShippedStatus { get; set; }
        /// <summary>
        /// null - Not Invoiced, false - partially Invoiced, true - full Invoiced
        /// It is to populate the grid of SO List
        /// </summary>
        public bool? InvoicedStatus { get; set; }
        #endregion

        /// <summary>
        /// if Directly shipped, SalesOrderNumber is null but ShipmentNumber will be there. here SO & Shipment 1:1 relation
        /// </summary>
        public string ShipmentNumber { get; set; }

        public string QuotationNumber { get; set; }
    }
}
