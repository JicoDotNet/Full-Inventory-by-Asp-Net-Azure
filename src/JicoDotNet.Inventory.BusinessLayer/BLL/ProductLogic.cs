using DataAccess.Sql;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.AzureStorage.Blob;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class ProductLogic : DBManager
    {
        public ProductLogic(ICommonLogicHelper commonObj) : base(commonObj) { }

        public string TypeSet(IProductType productType)
        {
            var queryType = productType.ProductTypeId > 0 ? "UPDATE" : "INSERT";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductTypeId", productType.ProductTypeId),
                new NameValuePair("@ProductTypeName", productType.ProductTypeName),
                new NameValuePair("@Description", productType.Description),
                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetProductType]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public string TypeDeactivate(string productTypeId)
        {
            string queryType = "DEACTIVE";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductTypeId", productTypeId),


                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetProductType]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public IList<ProductType> TypeGet()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetProductType]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<ProductType>();
        }

        public string Set(IProduct product)
        {
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

                new NameValuePair("@Description", ((Product)product).Description?.Trim()),
                new NameValuePair("@IsPerishableProduct", product.IsPerishableProduct),
                new NameValuePair("@HasExpirationDate", product.HasExpirationDate),
                new NameValuePair("@HasBatchNo", product.HasBatchNo),
                new NameValuePair("@ProductImageUrl", product.ProductImageUrl),
                new NameValuePair("@UnitOfMeasureId", product.UnitOfMeasureId),
                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };

            string returnDs = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetProduct]", nvp, "@OutParam").ToString();
            return returnDs;
        }

        public string Deactivate(string productId)
        {
            string queryType = "DEACTIVE";

            INameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductId", productId),

                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", queryType)
            };
            IList<string> nvpOut = new List<string>
            {
                "@OutParam"
            };

            var returnDs = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetProduct]", nvp, nvpOut);
            return returnDs.FirstOrDefault()?.getValue?.ToString();
        }

        public IList<Product> Get(bool? isActive = null)
        {
            IList<Product> products = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetProduct]",
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
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetProduct]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "INTIME")
                }).ToList<Product>();
        }

        public IList<Product> GetOut()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetProduct]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "OUTTIME")
                }).ToList<Product>();
        }

        public IBlobResponseClient UploadImage(IBlobRequestClient blobRequest)
        {
            if (blobRequest != null && blobRequest.FileStream != null)
            {
                BlobManager = new AzureBlobAccess("MyCompany", CommonLogicObj.NoSqlConnectionString);
                string[] dirs =
                blobRequest.Directories = new string[] { "Product" };

                return BlobManager.Upload(blobRequest);
            }
            return null;
        }
    }
}
