using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class VendorModels
    {
        public VendorType _vendorType { get; set; }
        public List<VendorType> _vendorTypes { get; set; }

        public Vendor _vendor { get; set; }
        public List<Vendor> _vendors { get; set; }

        public Dictionary<string, string> _state { get; set; }
        public Dictionary<string, string> _companyType { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }

        public List<VendorBank> _vendorBanks { get; set; }
        public VendorBank _vendorBank { get; set; }
    }
}