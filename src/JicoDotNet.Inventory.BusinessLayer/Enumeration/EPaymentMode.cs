using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public enum EPaymentMode
    {
        Cash = 1,
        NEFT = 2,
        Cheque = 3,
        Wallet = 4,

        Others = 9
    }
}
