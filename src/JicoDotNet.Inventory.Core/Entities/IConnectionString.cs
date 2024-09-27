using DataAccess.Sql.Entity;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IConnectionString
    {
        ICommonRequestDto CommonObj { get; }
        ISqlDBAccess _sqlDBAccess { get; }
    }
}