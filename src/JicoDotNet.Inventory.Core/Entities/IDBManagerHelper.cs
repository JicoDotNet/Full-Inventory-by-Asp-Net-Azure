using DataAccess.Sql.Entity;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IDBManagerHelper
    {
        ICommonLogicHelper CommonLogicObj { get; }
        ISqlDBAccess _sqlDBAccess { get; }
    }
}
