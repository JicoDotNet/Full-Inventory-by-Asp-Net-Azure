using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class PurchaseReportModels
    {
        public List<Vendor> _vendors { get; set; }
        public List<VendorType> _vendorTypes { get; set; }
        public List<RVendorPurchase> _rVendorPurchase { get; set; }

        public List<Product> _products { get; set; }
        public List<ProductType> _productTypes { get; set; }
        public List<RProductPurchase> _rProductPurchase{ get; set; }
    }
}