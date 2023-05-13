using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public enum ELimitOn
    {
        None = 0,

        // Tenant wise Limit
        User = 1,               // null = unlimited or value
        Organization = 2,       // null = unlimited or value
        RechargedUser = 5,      // Not Implemented

        // Tenant's Company wise Limit
        Product = 11,           // null = unlimited or value
        MessurementUnit = 12,   // null = unlimited or value
        Warehouse = 13,         // null = unlimited or value
        Retail = 14,            // null or 0 = false || 1 = true

        // Tenant's Company and Month wise Limit
        Bill = 21,              // null = unlimited or value
        Invoice = 22,           // null = unlimited or value

        Email = 51,             // Not Implemented
        EmailRule = 52,         // Not Implemented
        SMS = 61,               // Not Implemented
        SMSRule = 62            // Not Implemented
    }
}