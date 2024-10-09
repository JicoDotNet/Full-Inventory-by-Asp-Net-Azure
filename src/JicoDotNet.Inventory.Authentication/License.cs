using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.Authentication
{
    public sealed class License
    {
        public string Application { get; set; }
        public string ApplicationNamespace { get; set; }
        public string[] Domains { get; set; }
        public string LicenseKey { get; set; }
        public string LicenseType { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string AppReleaseVersion { get; set; }
        public string LicenseReleaseVersion { get; set; }
    }
}
