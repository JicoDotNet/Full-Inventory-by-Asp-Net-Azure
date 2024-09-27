using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IProductAttribute
    {
        bool IsPerishable { get; set; }
        string BatchNo { get; set; }
        DateTime? ExpiryDate { get; set; }
    }
}
