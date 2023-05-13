using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ErrorModels
    {
        public int ErrorStatus { get; set; }
        public string ErrorCode { get; set; }
        public string RequestId { get; set; }
        public string Message { get; set; }
    }
}