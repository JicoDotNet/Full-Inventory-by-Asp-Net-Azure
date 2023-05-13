using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface IWareHouse : IActivity, IStatus,   IHRequest
    {
         
        long WareHouseId { get; set; }
        long BranchId { get; set; }
        string WareHouseName { get; set; }
        bool IsRetailCounter { get; set; }
        string Description { get; set; }
    }
}
