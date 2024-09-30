using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class BillType : IBillType
    {
        public long BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
