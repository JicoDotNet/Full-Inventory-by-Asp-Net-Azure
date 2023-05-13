using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IPurchaseType
    {
        long PurchaseTypeId { get; set; }
        string PurchaseTypeName { get; set; }
        string Description { get; set; }
    }
}
