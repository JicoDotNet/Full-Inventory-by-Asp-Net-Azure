using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class GoodsReceiveNoteModels
    {
        public List<GoodsReceiveNote> _goodsReceiveNotes { get; set; }

        public List<PurchaseType> _purchaseTypes { get; set; }
        public List<Vendor> _vendors { get; set; }
        public List<WareHouse> _wareHouses { get; set; }
        public List<PurchaseOrder> _purchaseOrders { get; set; }
        public Dictionary<bool, string> _YesNo { get; set; }
        public PurchaseOrder _purchaseOrder { get; set; }
        public CompanyBasic _company { get; set; }
        public Company _companyAddress { get; set; }
        public Config _config { get; set; }

        public Branch _branch { get; set; }

        //To Post Data
        public GoodsReceiveNote _goodsReceiveNote { get; set; }
        public List<GoodsReceiveNoteDetail> _goodsReceiveNoteDetails { get; set; }
        public List<Product> _products { get; set; }
        public Product _product { get; set; }
        public int _len { get; set; }
    }
}