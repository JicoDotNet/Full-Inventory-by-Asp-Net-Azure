using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class BillModels
    {
        public IBillType _billType { get; set; }
        public IList<BillType> _billTypes { get; set; }
        public IBill _bill { get; set; }
        public IList<BillDetail> _billDetails { get; set; }
        public IList<Bill> _bills { get; set; }
        public Config _config { get; set; }
        public IList<PurchaseOrder> _purchaseOrders { get; set; }
        public PurchaseOrder _purchaseOrder { get; set; }
        public EGSTType GSTType { get; set; }

        public Company _companyAddress { get; set; }
        public Vendor _vendor { get; set; }
    }
}