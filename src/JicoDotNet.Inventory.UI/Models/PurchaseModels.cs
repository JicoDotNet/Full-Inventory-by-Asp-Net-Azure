using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class PurchaseModels
    {
        public PurchaseType _purchaseType { get; set; }
        public List<PurchaseType> _purchaseTypes { get; set; }
    }
}