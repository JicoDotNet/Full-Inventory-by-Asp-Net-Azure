using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IVendorType : IActivity, IStatus,   IHRequest
    {
        long VendorTypeId { get; set; }
        string VendorTypeName { get; set; }
        string Description { get; set; }
    }
}
