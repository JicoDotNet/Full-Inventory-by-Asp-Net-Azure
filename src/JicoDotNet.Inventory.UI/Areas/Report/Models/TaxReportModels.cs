using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Report;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class TaxReportModels
    {
        public ICompanyBasic _companyBasic { get; set; }
        public List<ResponseGSTOutputResult> _rGSTOut { get; set; }
        public List<ResponseGSTInputResult> _rGSTIn { get; set; }
    }
}