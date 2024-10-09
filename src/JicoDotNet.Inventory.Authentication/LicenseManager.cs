using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace JicoDotNet.Inventory.Authentication
{
    public class LicenseManager
    {
        private string _licenseFilePath;

        public LicenseManager()
        {
            _licenseFilePath = HttpContext.Current.Server.MapPath("~/license.lic");
        }

        public License LoadLicense()
        {
            string lines;
            using (StreamReader reader = new StreamReader(_licenseFilePath))
            {
                lines = reader.ReadLine();
            }

            return JsonConvert.DeserializeObject<License>(lines);
        }

        public bool Validate(License license)
        {
            if (license.ExpiryDate < DateTime.Now)
            {
                return false; // License expired
            }

            // Additional license validation (e.g., license key validation, feature checks)
            return true;
        }
    }
}


