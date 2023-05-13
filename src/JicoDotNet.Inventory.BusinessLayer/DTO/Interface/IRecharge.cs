using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IRecharge
    {
        string PlanId { get; set; }
        string PlanCostId { get; set; }
        string RechargeByMode { get; set; }
        string RechargeBy { get; set; }
        DateTime RechargeDate { get; set; }
        DateTime? ExpiryDate { get; set; }
        string RechargeMode { get; set; }
        decimal Price { get; set; }
        bool IsFreePlan { get; set; }
        string Remarks { get; set; }
    }
}
