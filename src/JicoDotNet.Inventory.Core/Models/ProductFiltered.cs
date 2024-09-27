using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class ProductFiltered : Product
    {
        public string label { get; set; }
        public string value { get; set; }
    }
}
