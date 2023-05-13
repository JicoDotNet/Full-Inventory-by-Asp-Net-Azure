using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class SMSSendTrack : ISMSSendTrack, IActivity, IStatus, IHRequest
    {
        public long SmsSendId { get; set; }
        public DateTime SendTime { get; set; }
        public string UrlLink { get; set; }
        public string SmsContent { get; set; }
        public string MobileNo { get; set; }
        public bool IsMobileNoChanged { get; set; }
        public bool IsResend { get; set; }
        public string SmsFor { get; set; }
        public string ComponentIdentity { get; set; }


         
        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
        
        public string UserName { get; set; }
        public string RequestId { get; set; }
    }
}
