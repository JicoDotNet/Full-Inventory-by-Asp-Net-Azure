using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class PaymentInDetailType
    {
        public int Id { get; set; }
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFullReceived { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
