using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IPurchaseReturn
    {
        long PurchaseReturnId { get; set; }
        string PurchaseReturnNumber { get; set; }
        DateTime PurchaseReturnDate { get; set; }
        bool IsFullReturned { get; set; }
        string Reason { get; set; }
    }
}