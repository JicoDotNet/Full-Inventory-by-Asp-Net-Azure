using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom
{
    internal class PaymentOutDetailType
    {
        public int Id { get; set; }
        public long BillId { get; set; }
        public string BillNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFullPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
