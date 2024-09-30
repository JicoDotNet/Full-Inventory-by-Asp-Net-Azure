using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Report;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Report.Models
{
    public class TaxReportModels
    {
        public ICompanyBasic _companyBasic { get; set; }
        public List<ResponseGSTOutputResult> _rGSTOut { get; set; }
        public List<ResponseGSTInputResult> _rGSTIn { get; set; }
    }
}