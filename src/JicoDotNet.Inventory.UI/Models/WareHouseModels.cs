using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class WareHouseModels
    {
        public WareHouse _wareHouse { get; set; }
        public List<WareHouse> _wareHouses { get; set; }
        public List<Branch> _branches { get; set; }
        
        public bool _isRetailEligible { get; set; }
    }
}