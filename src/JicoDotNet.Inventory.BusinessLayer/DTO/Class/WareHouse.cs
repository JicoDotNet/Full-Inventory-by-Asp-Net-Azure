using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class WareHouse : IWareHouse, IBranch
    {
         
        public long WareHouseId { get; set; }
        public long BranchId { get; set; }
        public string WareHouseName { get; set; }
        public bool IsRetailCounter { get; set; }
        public string Description { get; set; }

        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime TransactionDate { get; set; }
        public bool IsActive { get; set; }
         
         
        public string RequestId { get; set; }
    }
}
