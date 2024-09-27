using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseTracking
    {
        string IBProductId { get; set; }
        string Data { get; set; }
        string PlanId { get; set; }
        string PlanCostId { get; set; }
        bool IsActive { get; set; }
        DateTime TransactionDate { get; set; }
        string TenantCode { get; set; }
    }
}
