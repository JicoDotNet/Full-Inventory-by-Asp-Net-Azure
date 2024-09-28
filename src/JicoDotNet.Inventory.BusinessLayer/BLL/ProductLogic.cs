using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class ProductLogic : ConnectionString
    {
        public ProductLogic(ICommonRequestDto commonObj) : base(commonObj) { }

        public string TypeSet(IProductType productType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            var queryType = productType.ProductTypeId > 0 ? "UPDATE" : "INSERT";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductTypeId", productType.ProductTypeId),
                new NameValuePair("@ProductTypeName", productType.ProductTypeName),
                new NameValuePair("@Description", productType.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetProductType]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public string TypeDeactivate(string productTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string queryType = "DEACTIVE";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductTypeId", productTypeId),
                 
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetProductType]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public IList<ProductType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetProductType]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<ProductType>();
        }

        public string Set(IProduct product)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            var queryType = product.ProductId > 0 ? "UPDATE" : "INSERT";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductId", product.ProductId),                 
                 
                new NameValuePair("@ProductTypeId", product.ProductTypeId),
                new NameValuePair("@ProductInOut", product.ProductInOut),
                new NameValuePair("@Brand", product.Brand),
                new NameValuePair("@ProductName", product.ProductName),
                new NameValuePair("@ProductCode", product.ProductCode),

                new NameValuePair("@IsGoods", product.IsGoods),
                new NameValuePair("@SKU", product.SKU),
                new NameValuePair("@PurchasePrice", product.PurchasePrice),
                new NameValuePair("@SalePrice", product.SalePrice),

                new NameValuePair("@HSNSAC", product.HSNSAC),
                new NameValuePair("@TaxPercentage", product.TaxPercentage),

                new NameValuePair("@Description", product.Description?.Trim()),
                new NameValuePair("@IsPerishableProduct", product.IsPerishableProduct),
                new NameValuePair("@HasExpirationDate", product.HasExpirationDate),
                new NameValuePair("@HasBatchNo", product.HasBatchNo),
                new NameValuePair("@ProductImageUrl", product.ProductImageUrl),
                new NameValuePair("@UnitOfMeasureId", product.UnitOfMeasureId),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetProduct]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public string Deactivate(string productId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string queryType = "DEACTIVE";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductId", productId),
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };
            IList<string> nvpOut = new List<string>
            {
                "@OutParam"
            };

            var returnDs = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetProduct]", nvp, nvpOut);
            return returnDs.FirstOrDefault()?.getValue?.ToString();
        }

        public IList<Product> Get(bool? isActive = null)
        {
            IList<Product> products = new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetProduct]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<Product>();
            if (isActive != null)
            {
                if (isActive.Value)
                    return products.Where(a => a.IsActive).ToList();
                if (!isActive.Value)
                    return products.Where(a => !a.IsActive).ToList();
            }
            return products;
        }

        public IList<Product> GetIn()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetProduct]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "INTIME")
                }).ToList<Product>();
        }

        public IList<Product> GetOut()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetProduct]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "OUTTIME")
                }).ToList<Product>();
        }

        public string UploadImage(HttpPostedFileBase httpFileBase)
        {
            if (httpFileBase != null)
            {
                BlobManager = new ExecuteBlobManager("MyCompany", CommonObj.NoSqlConnectionString);
                string[] dirs = { "Product" };
                return BlobManager.UploadFile(httpFileBase, dirs, CommonObj.RequestId);
            }
            return null;
        }
    }
}
