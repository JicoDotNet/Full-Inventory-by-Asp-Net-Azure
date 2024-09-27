using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISessionCredentials
    {
        string Menu { get; set; }
        DateTime? ExpiryDate { get; set; }
    }
}
