using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    interface IBillType
    {
        long BillTypeId { get; set; }
        string BillTypeName { get; set; }
        string Description { get; set; }
         
    }
}
