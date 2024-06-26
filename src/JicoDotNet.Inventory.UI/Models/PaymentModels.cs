using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class PaymentModels
    {
        public PaymentType _paymentType { get; set; }
        public List<PaymentType> _paymentTypes { get; set; }

        public List<PaymentOut> _paymentOuts { get; set; }

        public Config _config { get; set; }
        public List<Vendor> _vendors { get; set; }
        public Vendor _vendor { get; set; }
        public List<VendorBank> _vendorBanks { get; set; }
        public List<Bill> _bills { get; set; }
        public List<PaymentOutDetail> _paymentOutDetails { get; set; }
        public IDictionary<short, string> _paymentMode { get; set; }

        public Bill _bill { get; set; }
        public PaymentOutDetail _paymentOutDetail { get; set; }



        public List<PaymentIn> _paymentIns { get; set; }

        public List<Customer> _customers { get; set; }
        public Customer _customer { get; set; }
        public List<CompanyBank> _companyBanks { get; set; }
        public List<Invoice> _invoices { get; set; }
        public List<PaymentInDetail> _paymentInDetails { get; set; }

        public Invoice _invoice { get; set; }
        public PaymentInDetail _paymentInDetail { get; set; }
    }
}