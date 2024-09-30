using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class UnitOfMeasure : IUnitOfMeasure, IDtoHeader
    {
        public long UnitOfMeasureId { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }


        public string RequestId { get; set; }
    }
}
