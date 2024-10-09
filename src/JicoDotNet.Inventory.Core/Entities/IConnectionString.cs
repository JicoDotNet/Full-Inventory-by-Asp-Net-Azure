using DataAccess.Sql.Entity;
using JicoDotNet.Authentication.Interfaces;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IConnectionString
    {
        ICommonRequestDto CommonObj { get; }
        ISqlDBAccess _sqlDBAccess { get; }
    }
}