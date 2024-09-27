using JicoDotNet.Inventory.Core.Common;

namespace JicoDotNet.Inventory.Core.Report.Interface
{
    public interface IRequestProductPurchaseParam
    {
        long ProductId { get; set; }
        long ProductTypeId { get; set; }
        DateRange SearchDate { get; set; }
    }
}
