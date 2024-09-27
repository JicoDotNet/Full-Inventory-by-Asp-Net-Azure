using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class PurchaseOrderModels
    {
        public string _draftId { get; set; }
        public List<PurchaseType> _purchaseTypes { get; set; }
        public List<Branch> _branches { get; set; }
        public List<Vendor> _vendors { get; set; }
        public List<Product> _products { get; set; }
        public Config _config { get; set; }
        public PurchaseOrder _purchaseOrder { get; set; }
        public List<PurchaseOrder> _purchaseOrders { get; set; }
        public Vendor _vendor { get; set; }
        public ICompanyBasic _company { get; set; }
        public Company _companyAddress { get; set; }
    }
}