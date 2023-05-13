using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class PaymentOutDetail : IPaymentOutDetail, IActivity, IStatus,   IHRequest
    {
        public long PaymentOutDetailId { get; set; }
        public long PaymentOutId { get; set; }
        public long BillId { get; set; }
        public string BillNumber { get; set; }
        public decimal Amount { get; set; }
        public bool IsFullPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }        

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }

        /// <summary>
        /// Previous Paid Amount
        /// </summary>
        //public decimal PaidAmount { get; set; }
    }
}
