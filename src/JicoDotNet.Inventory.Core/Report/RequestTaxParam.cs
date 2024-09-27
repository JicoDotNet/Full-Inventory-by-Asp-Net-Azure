using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Report.Interface;

namespace JicoDotNet.Inventory.Core.Report
{
    public class RequestTaxParam : IRequestTaxParam
    {
        public DateRange SearchDate { get; set; }
        public bool? PaymentStatus { get; set; }
    }
}
