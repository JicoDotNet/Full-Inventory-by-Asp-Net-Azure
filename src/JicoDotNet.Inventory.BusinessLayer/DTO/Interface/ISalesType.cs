using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ISalesType
    {
        long SalesTypeId { get; set; }
         
        string SalesTypeName { get; set; }
        string Description { get; set; }
    }
}
