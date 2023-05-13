using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class LoginLog : Logger, ILoginLog
    {
        public LoginLog() {ActivityDate = GenericLogic.IstNow; }
        public DateTime? ActivityDate { get; set; }
    }
}
