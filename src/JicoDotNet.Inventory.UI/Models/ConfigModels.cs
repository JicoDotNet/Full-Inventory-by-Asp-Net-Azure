using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ConfigModels
    {
        public Config _config { get; set; }
        public ICompanyBasic _company { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
    }
}