using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IDataTracking : IHRequest, IActivity
    {
        string Data { get; set; }
    }
}
