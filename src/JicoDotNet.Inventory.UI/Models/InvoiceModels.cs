using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class InvoiceModels
    {
        public InvoiceType _invoiceType { get; set; }
        public List<InvoiceType> _invoiceTypes { get; set; }

        public List<Invoice> _invoices { get; set; }
        public List<InvoiceDetail> _invoiceDetails { get; set; }
        public Config _config { get; set; }
        public List<SalesOrder> _salesOrders { get; set; }
        public SalesOrder _salesOrder { get; set; }
        public EGSTType GSTType { get; set; }
        public ICompanyBasic _company { get; set; }

        public Company _companyAddress { get; set; }
        public CompanyBank _companyBank { get; set; }
        public Invoice _invoice { get; set; }
        public string _invoiceHtml { get; set; }
        public Dictionary<string, string> _customPropertyValue { get; set; }
        public Customer _customer { get; set; }
        

        public DateTime _dateLimit { get; set; }
    }
}