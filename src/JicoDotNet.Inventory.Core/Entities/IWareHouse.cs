﻿using JicoDotNet.Inventory.Core.Entities.Inner;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IWareHouse : IGenericDescription
    {

        long WareHouseId { get; set; }
        long BranchId { get; set; }
        string WareHouseName { get; set; }
        bool IsRetailCounter { get; set; }
    }
}
