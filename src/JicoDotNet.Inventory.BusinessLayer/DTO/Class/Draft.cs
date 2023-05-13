using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class Draft : TableEntity, IDraft, IActivity, IStatus,   IHRequest  
    {
        public long UserId { get; set; }
        public string DraftData { get; set; }
        public string DraftType { get; set; }

        public EDraft _DraftType { get; set; }

         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
