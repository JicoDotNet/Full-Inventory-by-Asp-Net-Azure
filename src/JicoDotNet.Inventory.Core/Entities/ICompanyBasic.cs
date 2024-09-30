using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICompanyBasic : IGstEntity
    {
        string CompanyName { get; set; }
        string StateCode { get; set; }
    }
}