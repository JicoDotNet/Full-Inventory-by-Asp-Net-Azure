using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IVendorBank
    {
        long VendorBankId { get; set; }
        long VendorId { get; set; }
    }
}
