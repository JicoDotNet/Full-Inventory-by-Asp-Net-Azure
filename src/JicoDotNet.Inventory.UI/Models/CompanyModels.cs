using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class CompanyModels
    {
        public ISessionCredential _sessionCredential { get; set; }
        public Dictionary<string, string> _state { get; set; }
        public Dictionary<string, string> _companyType { get; set; }
        public Company _company { get; set; }
        public Config _config { get; set; }
        public Company _companyAddress { get; set; }
        public Dictionary<bool, string> _YesNo { get; set; }
        public CompanyBank _companyBank { get; set; }
        public List<CompanyBank> _companyBanks { get; set; }
    }
}