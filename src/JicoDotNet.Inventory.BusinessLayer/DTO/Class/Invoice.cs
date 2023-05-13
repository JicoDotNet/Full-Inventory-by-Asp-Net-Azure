using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class Invoice : IInvoice, ICustomer, IActivity, IStatus,   IHRequest
    {
        public long InvoiceId { get; set; }
        public long SalesOrderId { get; set; }
        public long InvoiceTypeId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public bool IsCustomizedInvoiceNumber { get; set; }
        public bool IsFullInvoiced { get; set; }
         
        public long CustomerId { get; set; }
        public bool IsGstApplicable { get; set; }
        public string GSTNumber { get; set; }
        public string GSTStateCode { get; set; }
        public short GSTType { get; set; }
         
        public decimal InvoicedAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
         
        public string VehicleNumber { get; set; }
        public string HandOverPerson { get; set; }
        public string HandOverPersonMobile { get; set; }
         
        public string Remarks { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }

        public long CustomerTypeId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string StateCode { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string PANNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<InvoiceDetail> InvoiceDetails { get; set; }

        // Bill Grid Show (spGetInvoice - LIST)
        public string SalesOrderNumber { get; set; }
        public string ShipmentNumber { get; set; }

        /// <summary>
        /// Previous Receive Amount, for payment
        /// </summary>
        public decimal ReceivedAmount { get; set; }

        public bool? PaymentStatus { get; set; }
    }
}
