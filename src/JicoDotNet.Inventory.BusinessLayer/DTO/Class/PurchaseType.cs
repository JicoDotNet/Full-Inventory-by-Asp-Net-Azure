using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class PurchaseType : IPurchaseType, IActivity, IStatus,   IHRequest  
    {
        public long PurchaseTypeId { get; set; }        
        public string PurchaseTypeName { get; set; }
        public string Description { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
