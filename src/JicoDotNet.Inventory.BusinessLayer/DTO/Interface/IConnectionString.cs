using DataAccess.Sql.Entity;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IConnectionString
    {
        ICommonRequestDto CommonObj { get; }
        ISqlDBAccess _sqlDBAccess { get; }
    }
}