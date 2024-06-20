using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
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
        public ProductLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public string TypeSet(ProductType productType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (productType.ProductTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductTypeId", productType.ProductTypeId),
                new NameValuePair("@ProductTypeName", productType.ProductTypeName),
                new NameValuePair("@Description", productType.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProductType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string ProductTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "DEACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductTypeId", ProductTypeId),
                 
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProductType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<ProductType> TypeGet()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProductType]",
                new NameValuePairs
                {
                    new NameValuePair("@QueryType", "ALL")
                }).ToList<ProductType>();
        }

        public string Set(IProduct product)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (product.ProductId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

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
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProduct]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string ProductId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "DEACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@ProductId", ProductId),
                 
                 
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetProduct]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<Product> Get(bool? IsActive = null)
        {
            List<Product> products = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProduct]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "ALL")
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
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "INTIME")
                }).ToList<Product>();
        }

        public List<Product> GetOut()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetProduct]",
                new NameValuePairs
                {
                     
                     
                    new NameValuePair("@QueryType", "OUTTIME")
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
