using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IStockAdjustReason
    {
        long AdjustReasonId { get; set; }
        string AdjustReason { get; set; }
        bool IsDefault { get; set; }

        bool IsStockIncreasing { get; set; }
    }
}
