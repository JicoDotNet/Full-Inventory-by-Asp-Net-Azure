using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;

namespace JicoDotNet.Inventory.Core.Common.Auth
{
    public class AccountAuthentication : IAccountAuthentication
    {
        public ISessionCredential credential { get; set; }
        public ELoginStatus eLoginStatus { get; set; }
    }
}