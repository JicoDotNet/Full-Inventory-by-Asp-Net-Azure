using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

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
        public IList<Bill> _bills { get; set; }
        public List<PaymentOutDetail> _paymentOutDetails { get; set; }
        public IDictionary<short, string> _paymentMode { get; set; }

        public IBill _bill { get; set; }
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