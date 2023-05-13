using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public struct DateRange : IDateRange
    {
        /// <summary>
        ///     Gets the start date component of the date range.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        ///     Gets the end date component of the date range.
        /// </summary>
        public DateTime? EndDate { get; set; }

        public IList<DateTime> Dates
        {
            get
            {
                if (StartDate != null && EndDate != null)
                {
                    if (EndDate > StartDate)
                    {
                        if (StartDate != null)
                        {
                            var startDate = StartDate.Value;
                            return Enumerable.Range(0, Days)
                                    .Select(offset => startDate.AddDays(offset))
                                    .ToList();
                        }
                    }
                    else if (EndDate == StartDate)
                    {
                        if (StartDate != null)
                        {
                            return new List<DateTime>
                            {
                                StartDate.Value
                            };
                        }
                    }
                }
                return new List<DateTime>();
            }
        }

        public int Days
        {
            get
            {
                if (StartDate != null && EndDate != null)
                {
                    if (EndDate > StartDate)
                    {
                        return (EndDate - StartDate).Value.Days;
                    }
                    else if (EndDate == StartDate)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }

        public bool IsInOneYearRange()
        {
            if (StartDate != null && EndDate != null)
            {
                ////new code
                if (EndDate.Value.AddDays(1).AddSeconds(-1) < StartDate.Value.AddYears(1)
                    && Days > 0)
                {
                    EndDate = EndDate.Value.AddDays(1).AddSeconds(-1);
                    return true;
                }
                else
                    return false;

                ////old code
                //if ((DateTime.IsLeapYear(StartDate.Value.Year) && StartDate.Value.Month < 3) ||
                //        (DateTime.IsLeapYear(EndDate.Value.Year) && EndDate.Value.Month > 2))
                //    return Days < 366;  // Leapyear
                //else
                //    return Days < 365;
            }
            return false;
        }
    }
}
