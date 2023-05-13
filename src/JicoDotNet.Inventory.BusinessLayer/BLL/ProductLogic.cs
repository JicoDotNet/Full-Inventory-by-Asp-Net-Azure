using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class ProductLogic : ConnectionString
    {
        public ProductLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public string TypeSet(ProductType productType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (productType.ProductTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@ProductTypeId", productType.ProductTypeId),
                new nameValuePair("@ProductTypeName", productType.ProductTypeName),
                new nameValuePair("@Description", productType.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProductType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string ProductTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "DEACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@ProductTypeId", ProductTypeId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProductType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<ProductType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProductType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<ProductType>();
        }

        public string Set(Product product)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (product.ProductId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@ProductId", product.ProductId),
                 
                 
                new nameValuePair("@ProductTypeId", product.ProductTypeId),
                new nameValuePair("@ProductInOut", product.ProductInOut),
                new nameValuePair("@Brand", product.Brand),
                new nameValuePair("@ProductName", product.ProductName),
                new nameValuePair("@ProductCode", product.ProductCode),

                new nameValuePair("@IsGoods", product.IsGoods),
                new nameValuePair("@SKU", product.SKU),
                new nameValuePair("@PurchasePrice", product.PurchasePrice),
                new nameValuePair("@SalePrice", product.SalePrice),

                new nameValuePair("@HSNSAC", product.HSNSAC),
                new nameValuePair("@TaxPercentage", product.TaxPercentage),

                new nameValuePair("@Description", product.Description?.Trim()),
                new nameValuePair("@IsPerishableProduct", product.IsPerishableProduct),
                new nameValuePair("@HasExpirationDate", product.HasExpirationDate),
                new nameValuePair("@HasBatchNo", product.HasBatchNo),
                new nameValuePair("@ProductImageUrl", product.ProductImageUrl),
                new nameValuePair("@UnitOfMeasureId", product.UnitOfMeasureId),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProduct]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string ProductId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "DEACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@ProductId", ProductId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProduct]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<Product> Get(bool? IsActive = null)
        {
            List<Product> products = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProduct]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<Product>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return products.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return products.Where(a => !a.IsActive).ToList();
            }
            return products;
        }

        public List<Product> GetIn()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProduct]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "INTIME")
                }).ToList<Product>();
        }

        public List<Product> GetOut()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProduct]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "OUTTIME")
                }).ToList<Product>();
        }

        public string UploadImage(HttpPostedFileBase httpFileBase)
        {
            if (httpFileBase != null)
            {
                _blobManager = new ExecuteBlobManager("MyCompany", CommonObj.NoSqlConnectionString);
                string[] Dirs = { "Product" };
                return _blobManager.UploadFile(httpFileBase, Dirs, CommonObj.RequestId);
            }
            else
                return null;
        }
    }
}
