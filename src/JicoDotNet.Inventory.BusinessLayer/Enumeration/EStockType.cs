using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public enum EStockType
    {
        Transfer = 0,
        GRN = 1,
        Shipment = -1,

        SalesReturn = 2,
        PurchaseReturn = -2,

        OpeningStock = 10,
        ClosingStock = -10,

        AdjustIncrease = 20,
        AdjustDecrease = -20
    }
}