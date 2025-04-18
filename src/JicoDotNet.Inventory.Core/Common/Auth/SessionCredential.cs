using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using System;

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