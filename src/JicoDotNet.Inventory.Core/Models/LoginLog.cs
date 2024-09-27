using System;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class LoginLog : Logger, ILoginLog
    {
        public DateTime? ActivityDate { get; set; }
    }
}