using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPurchaseTracking : ITableEntity
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
