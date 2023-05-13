using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report
{
    public class PCustomerSales
    {
        public long CustomerId { get; set; }
        public long CustomerTypeId { get; set; }
        public DateRange SearchDate { get; set; }
        public bool? ForRetail { get; set; }
    }
}
