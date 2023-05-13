using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    interface ICustomerType: IActivity, IStatus,   IHRequest
    {
        long CustomerTypeId { get; set; }
        string CustomerTypeName { get; set; }
        string Description { get; set; }
    }
}
