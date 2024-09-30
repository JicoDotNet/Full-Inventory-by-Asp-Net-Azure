using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class GoodsReceiveNoteModels
    {
        public List<GoodsReceiveNote> _goodsReceiveNotes { get; set; }

        public List<PurchaseType> _purchaseTypes { get; set; }
        public List<Vendor> _vendors { get; set; }
        public List<WareHouse> _wareHouses { get; set; }
        public List<PurchaseOrder> _purchaseOrders { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
        public PurchaseOrder _purchaseOrder { get; set; }
        public ICompanyBasic _company { get; set; }
        public Company _companyAddress { get; set; }
        public Config _config { get; set; }

        public Branch _branch { get; set; }

        //To Post Data
        public GoodsReceiveNote _goodsReceiveNote { get; set; }
        public List<GoodsReceiveNoteDetail> _goodsReceiveNoteDetails { get; set; }
        public IList<Product> _products { get; set; }
        public Product _product { get; set; }
        public int _len { get; set; }
    }
}