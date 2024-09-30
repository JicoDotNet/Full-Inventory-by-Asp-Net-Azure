using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class InvoiceType : IInvoiceType, IDtoHeader
    {
        public long InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        public string RequestId { get; set; }
    }
}
