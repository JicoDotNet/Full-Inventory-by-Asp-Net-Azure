using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Common
{
    public class WebConfigAppSettings : IWebConfigAppSettings
    {
        public string UserFullName { get; protected set; }
        public string UserEmail { get; protected set; }
        public string Password { get; protected set; }
        public string AllowedStartIP { get; protected set; }
        public string AllowedEndIP { get; protected set; }

        public string CompanyName { get; protected set; }
        public string GSTNumber { get; protected set; }

        public string CompanyEmail { get; protected set; }
        public string CompanyMobile { get; protected set; }
        public string CompanyAddress { get; protected set; }
        public string CompanyPINCode { get; protected set; }
        public string CompanyCity { get; protected set; }
        public string CompanyWebsite { get; protected set; }
    }
}
