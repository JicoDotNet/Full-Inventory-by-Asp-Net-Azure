using JicoDotNet.Inventory.Core.Common;

namespace JicoDotNet.Inventory.Core.Report.Interface
{
    public interface IRequestTaxParam
    {
        DateRange SearchDate { get; set; }
        bool? PaymentStatus { get; set; }
    }
}
