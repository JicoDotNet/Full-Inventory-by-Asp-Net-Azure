using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class PurchaseModels
    {
        public PurchaseType _purchaseType { get; set; }
        public List<PurchaseType> _purchaseTypes { get; set; }
    }
}