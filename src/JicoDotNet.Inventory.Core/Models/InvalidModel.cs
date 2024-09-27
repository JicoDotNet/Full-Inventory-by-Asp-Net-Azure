using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class InvalidModel : IInvalidModel
    {
        public object ModelData { get; set; }
    }
}
