using System;
using JicoDotNet.Inventory.Core.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace JicoDotNet.Inventory.Core.Common.Auth
{
    public class SessionCredential : TableEntity, ISessionCredential
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }

        public string Token { get; set; }
        public DateTime? TokenDate { get; set; }
    }
}