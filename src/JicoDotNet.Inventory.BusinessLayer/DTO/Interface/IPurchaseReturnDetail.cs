using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPurchaseReturnDetail
    {
        long PurchaseReturnDetailId { get; set; }
        string PurchaseReturnNumber { get; set; }
        decimal ReturnedQuantity { get; set; }
    }
}