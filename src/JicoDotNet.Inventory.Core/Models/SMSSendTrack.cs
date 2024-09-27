using System;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Models
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
