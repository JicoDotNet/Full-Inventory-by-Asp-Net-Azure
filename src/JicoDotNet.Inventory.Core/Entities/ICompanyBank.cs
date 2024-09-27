using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICompanyBank : IBankCommon
    {
        long CompanyBankId { get; set; }
        bool IsPrintable { get; set; }
    }
}
