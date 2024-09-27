using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Report.Interface;

namespace JicoDotNet.Inventory.Core.Report
{
    public class RequestCustomerSalesParam : IRequestCustomerSalesParam
    {
        public long CustomerId { get; set; }
        public long CustomerTypeId { get; set; }
        public DateRange SearchDate { get; set; }
        public bool? ForRetail { get; set; }
    }
}
