using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.UI.Models
{
    public class SessionCredentialCompanyModels
    {
        public ISessionCredential _sessionCredential { get; set; }
        public ICompanyBasic _company { get; set; }
    }
}