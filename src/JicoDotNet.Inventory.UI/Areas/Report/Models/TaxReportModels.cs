using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class TaxReportModels
    {
        public CompanyBasic _companyBasic { get; set; }
        public List<RGSTOutput> _rGSTOut { get; set; }
        public List<RGSTInput> _rGSTIn { get; set; }
    }
}