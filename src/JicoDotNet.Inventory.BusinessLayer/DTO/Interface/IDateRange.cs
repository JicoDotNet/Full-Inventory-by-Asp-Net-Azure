using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IDateRange
    {
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        IList<DateTime> Dates { get; }
        int Days { get; }
        bool IsInOneYearRange();
    }
}
