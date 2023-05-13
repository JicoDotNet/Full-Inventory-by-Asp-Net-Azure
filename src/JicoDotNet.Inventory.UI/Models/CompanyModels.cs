using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class CompanyModels
    {
        public SessionCredential _sessionCredential { get; set; }
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