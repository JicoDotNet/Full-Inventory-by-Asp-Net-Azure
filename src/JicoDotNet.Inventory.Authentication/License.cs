using System;
namespace JicoDotNet.Inventory.Authentication
{
    public sealed class AuthLicense
    {
        public string Application { get; set; }
        public string ApplicationNamespace { get; set; }
        public string[] Domains { get; set; }
        public string SqlSchema { get; set; }
        public string LicenseKey { get; set; }
        public string LicenseType { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string AppReleaseVersion { get; set; }
        public string LicenseReleaseVersion { get; set; }
    }
}
