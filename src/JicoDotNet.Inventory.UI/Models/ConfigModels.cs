using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ConfigModels
    {
        public Config _config { get; set; }
        public CompanyBasic _company { get; set; }
        public Dictionary<bool, string> _YesNo { get; set; }
    }
}