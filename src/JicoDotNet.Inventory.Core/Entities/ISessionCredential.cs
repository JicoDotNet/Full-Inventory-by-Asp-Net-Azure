using Microsoft.WindowsAzure.Storage.Table;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ISessionCredential : ITableEntity, IToken
    {
    }
}
