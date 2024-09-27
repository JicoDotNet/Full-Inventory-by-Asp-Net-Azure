using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface ICommonRequestDto : IHRequest
    {
        string Token { get; set; }
        object SqlConnectionString { get; set; }
        object NoSqlConnectionString { get; set; }
    }
}
