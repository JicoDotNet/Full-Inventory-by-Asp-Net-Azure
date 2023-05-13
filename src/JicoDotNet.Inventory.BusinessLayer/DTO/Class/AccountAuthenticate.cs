using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class AccountAuthentication
    {
        public SessionCredential credential { get; set; }
        public ELoginStatus eLoginStatus { get; set; }
    }
}