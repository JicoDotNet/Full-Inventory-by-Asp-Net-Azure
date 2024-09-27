using System.Collections.Generic;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ProductModels
    {
        public IProductType _productType { get; set; }
        public IList<ProductType> _productTypes { get; set; }

        
        public IList<Product> _products { get; set; }
        public IProduct _product { get; set; }
        public List<UnitOfMeasure> _unitOfMeasures { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
        public IDictionary<int, string> _ProductCategory { get; set; }
        public Config _config { get; set; }
        public List<WareHouse> _wareHouses { get; set; }
    }
}