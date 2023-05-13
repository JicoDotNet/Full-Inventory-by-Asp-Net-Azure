using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class PProductPurchase
    {
        public long ProductId { get; set; }
        public long ProductTypeId { get; set; }
        public DateRange SearchDate { get; set; }
    }
}
