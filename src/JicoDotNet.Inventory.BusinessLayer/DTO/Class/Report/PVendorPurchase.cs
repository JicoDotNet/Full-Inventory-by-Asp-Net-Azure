using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class PVendorPurchase
    {
        public long VendorId { get; set; }
        public long VendorTypeId { get; set; }
        public DateRange SearchDate { get; set; }
    }
}
