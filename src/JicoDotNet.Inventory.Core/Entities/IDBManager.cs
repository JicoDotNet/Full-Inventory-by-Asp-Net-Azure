using DataAccess.Sql.Entity;
namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IDBManager
    {
        ISqlDBAccess _sqlDBAccess { get; }
    }
}