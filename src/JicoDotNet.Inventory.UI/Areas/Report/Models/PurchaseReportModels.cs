using System.Collections.Generic;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using JicoDotNet.Inventory.Core.Report;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class PurchaseReportModels
    {
        public List<Vendor> _vendors { get; set; }
        public List<VendorType> _vendorTypes { get; set; }
        public List<ResponseVendorPurchaseResult> _rVendorPurchase { get; set; }

        public IList<Product> _products { get; set; }
        public IList<ProductType> _productTypes { get; set; }
        public IList<ResponseProductPurchaseResult> _rProductPurchase{ get; set; }
    }
}