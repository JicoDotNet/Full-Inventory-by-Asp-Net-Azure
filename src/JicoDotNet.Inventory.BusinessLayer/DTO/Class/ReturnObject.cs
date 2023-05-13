using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class ReturnObject : IReturnObject
    {
        public bool Status { get; set; }
        public object ReturnData { get; set; }
        public int ResponseStatus { get; set; }
        public string Message { get; set; }
    }
}
