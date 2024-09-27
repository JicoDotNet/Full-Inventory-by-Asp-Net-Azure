using JicoDotNet.Inventory.Core.Common;

namespace JicoDotNet.Inventory.Core.Report.Interface
{
    public interface IRequestCustomerSalesParam
    {
        long CustomerId { get; set; }
        long CustomerTypeId { get; set; }
        DateRange SearchDate { get; set; }
        bool? ForRetail { get; set; }
    }
}
