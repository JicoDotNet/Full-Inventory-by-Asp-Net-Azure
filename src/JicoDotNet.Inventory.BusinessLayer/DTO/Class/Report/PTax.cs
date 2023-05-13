using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class PTax
    {
        public DateRange SearchDate { get; set; }
        public bool? PaymentStatus { get; set; }
    }
}
