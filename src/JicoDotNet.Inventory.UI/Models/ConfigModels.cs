using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ConfigModels
    {
        public Config _config { get; set; }
        public ICompanyBasic _company { get; set; }
        public Dictionary<bool, string> _YesNo { get; set; }
    }
}