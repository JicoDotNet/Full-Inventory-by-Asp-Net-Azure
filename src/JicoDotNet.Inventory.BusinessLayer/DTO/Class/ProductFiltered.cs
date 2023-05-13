using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class ProductFiltered : Product
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}
