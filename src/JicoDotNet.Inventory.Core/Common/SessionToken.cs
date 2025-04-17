using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Common
{
    public class SessionToken : ISessionToken
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public DateTime? TokenDate { get; set; }
        public string Key { get; set; }
    }
}
