using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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
