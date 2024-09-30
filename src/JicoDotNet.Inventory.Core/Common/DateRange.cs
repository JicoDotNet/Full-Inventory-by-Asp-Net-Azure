using JicoDotNet.Inventory.Core.Entities.Inner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JicoDotNet.Inventory.Core.Common
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
                if (StartDate == null || EndDate == null) return 0;
                if (EndDate > StartDate)
                {
                    return (EndDate - StartDate).Value.Days;
                }

                return EndDate == StartDate ? 1 : 0;
            }
        }

        public bool IsInOneYearRange
        {
            get
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

                    return false;

                    ////old code
                    //if ((DateTime.IsLeapYear(StartDate.Value.Year) && StartDate.Value.Month < 3) ||
                    //        (DateTime.IsLeapYear(EndDate.Value.Year) && EndDate.Value.Month > 2))
                    //    return Days < 366;  // Leap-year
                    //else
                    //    return Days < 365;
                }
                return false;
            }

        }
    }
}
