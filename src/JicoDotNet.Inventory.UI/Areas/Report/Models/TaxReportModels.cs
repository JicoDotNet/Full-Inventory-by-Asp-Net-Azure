using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class TaxReportModels
    {
        public ICompanyBasic _companyBasic { get; set; }
        public List<RGSTOutput> _rGSTOut { get; set; }
        public List<RGSTInput> _rGSTIn { get; set; }
    }
}