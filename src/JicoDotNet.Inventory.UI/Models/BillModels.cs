using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class BillModels
    {
        public BillType _billType { get; set; }
        public List<BillType> _billTypes { get; set; }
        public Bill _bill { get; set; }
        public List<BillDetail> _billDetails { get; set; }
        public List<Bill> _bills { get; set; }
        public Config _config { get; set; }
        public List<PurchaseOrder> _purchaseOrders { get; set; }
        public PurchaseOrder _purchaseOrder { get; set; }
        public EGSTType GSTType { get; set; }

        public Company _companyAddress { get; set; }
        public Vendor _vendor { get; set; }
    }
}