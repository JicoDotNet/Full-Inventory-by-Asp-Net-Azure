using JicoDotNet.Validator.Interfaces;
using JicoDotNet.Inventory.Core.Enumeration;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IAccountAuthentication
    {
        ISessionCredential credential { get; set; }
        ELoginStatus eLoginStatus { get; set; }
    }
}
