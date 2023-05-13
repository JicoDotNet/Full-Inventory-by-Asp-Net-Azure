using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public enum ELoginStatus
    {
        Error = 0,

        UserNameOrPasswordInvalid = 1,
        UserNotInIPRange = 2,
        IPAddressFormatError = 3,
        DuplicateLogin = 4,

        Success = 6
    }
}
