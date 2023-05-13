using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
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