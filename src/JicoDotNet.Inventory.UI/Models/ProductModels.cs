using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ProductModels
    {
        public ProductType _productType { get; set; }
        public List<ProductType> _productTypes { get; set; }

        
        public List<Product> _products { get; set; }
        public Product _product { get; set; }
        public List<UnitOfMeasure> _unitOfMeasures { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
        public IDictionary<int, string> _ProductCategory { get; set; }
        public Config _config { get; set; }
        public List<WareHouse> _wareHouses { get; set; }
    }
}