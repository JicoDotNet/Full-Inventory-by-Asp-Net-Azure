using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISMSSendTrack
    {
        long SmsSendId { get; set; }
        DateTime SendTime { get; set; }
        string UrlLink { get; set; }
        string SmsContent { get; set; }
        string MobileNo { get; set; }
        bool IsMobileNoChanged { get; set; }
        bool IsResend { get; set; }
        string SmsFor { get; set; }
        string ComponentIdentity { get; set; }
    }
}
