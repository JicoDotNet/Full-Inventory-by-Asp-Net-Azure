using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.Core.Entities.Inner
{
    public interface IDateRange
    {
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        IList<DateTime> Dates { get; }
        int Days { get; }

        bool IsInOneYearRange { get; }
    }
}
