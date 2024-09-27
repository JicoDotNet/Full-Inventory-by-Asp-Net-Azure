using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ConfigModels
    {
        public Config _config { get; set; }
        public ICompanyBasic _company { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
    }
}