using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Models
{
    public class LoginLog : Logger, ILoginLog
    {
        public DateTime? ActivityDate { get; set; }
    }
}