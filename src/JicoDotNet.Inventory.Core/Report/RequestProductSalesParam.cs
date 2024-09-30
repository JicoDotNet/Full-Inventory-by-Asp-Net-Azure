using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Report.Interface;

namespace JicoDotNet.Inventory.Core.Report
{
    public class RequestProductSalesParam : IRequestProductSalesParam
    {
        public long ProductId { get; set; }
        public long ProductTypeId { get; set; }
        public DateRange SearchDate { get; set; }
        public bool? ForRetail { get; set; }

    }
}
