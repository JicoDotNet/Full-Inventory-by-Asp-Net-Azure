using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class VendorType : IVendorType, IDtoHeader
    {
        public long VendorTypeId { get; set; }
        public string VendorTypeName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
