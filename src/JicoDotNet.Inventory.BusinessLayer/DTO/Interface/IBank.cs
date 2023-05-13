using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IBank
    {
        string AccountName { get; set; }
        string AccountNumber { get; set; }
        string BankName { get; set; }
        string IFSC { get; set; }
        string MICR { get; set; }
        string BranchName { get; set; }
        string BranchAddress { get; set; }
    }
}
