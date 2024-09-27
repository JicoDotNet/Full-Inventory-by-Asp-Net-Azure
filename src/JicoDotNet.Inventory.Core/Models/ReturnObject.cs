using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Models
{
    public class ReturnObject : IReturnObject
    {
        public bool Status { get; set; }
        public object ReturnData { get; set; }
        public int ResponseStatus { get; set; }
        public string Message { get; set; }
    }
}
