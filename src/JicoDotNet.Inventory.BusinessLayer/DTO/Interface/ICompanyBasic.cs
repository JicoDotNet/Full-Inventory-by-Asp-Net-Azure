using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ICompanyBasic
    {
        string CompanyName { get; set; }
        string StateCode { get; set; }
        bool IsGSTRegistered { get; set; }
        string GSTStateCode { get; set; }
        string GSTNumber { get; set; }
    }
}