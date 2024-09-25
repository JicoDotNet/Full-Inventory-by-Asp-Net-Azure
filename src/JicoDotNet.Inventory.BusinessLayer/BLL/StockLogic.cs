using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom;
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
    public class StockLogic : ConnectionString
    {
        public StockLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<Stock> Get(Stock stock)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 

                new nameValuePair("@WareHouseId", stock.WareHouseId),
                new nameValuePair("@ProductId", stock.ProductId),
                new nameValuePair("@GRNOrShipmentDate", stock.GRNOrShipmentDate > new DateTime(2001, 1, 1)?
                                                           (object)stock.GRNOrShipmentDate : DBNull.Value),
                new nameValuePair("@QueryType", "CURRENT")
            };
            return _sqlDBAccess.GetData("[dbo].[spGetStock]", nvp).ToList<Stock>();
        }

        public List<Stock> GetDetail(Stock stock)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 

                new nameValuePair("@WareHouseId", stock.WareHouseId),
                new nameValuePair("@ProductId", stock.ProductId),
                
                new nameValuePair("@QueryType", "DETAIL")
            };
            DataSet dataSet = _sqlDBAccess.GetDataSet("[dbo].[spGetStock]", nvp);
            List<Stock> stocks = dataSet.Tables[0].ToList<Stock>();
            List<StockDetail> stockDetails = dataSet.Tables[1].ToList<StockDetail>();

            foreach(Stock loopstock in stocks)
            {
                loopstock.StockDetails = stockDetails
                                            .Where(a => a.WareHouseId == loopstock.WareHouseId
                                                && a.ProductId == loopstock.ProductId).ToList();
            }
            return stocks;
        }

        public long TotalNonOpeningStockQuantity(long ProductId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            nameValuePairs nvp = new nameValuePairs()
            {
                 
                 

                new nameValuePair("@ProductId", ProductId),
                new nameValuePair("@QueryType", "NOTOPNINGSTOCK")
            };
            try
            {
                DataTable dt = _sqlDBAccess.GetData("[dbo].[spGetStock]", nvp);
                if (dt != null)
                    if (dt.Rows.Count > 0)
                        if (dt.Rows[0]["ProductQuantity"] != null)
                            return Convert.ToInt64(dt.Rows[0]["ProductQuantity"].ToString());
            }
            catch 
            {
            }
            return 0;
        }

        public string AddOpeningStock(List<StockDetail> stockDetails, long ProductId)
        {
            List<OpeningStockDetail> opnStkDetailTypes = new List<OpeningStockDetail>();
            int count = 1;
            stockDetails.ForEach(stk =>
            {
                if (stk.Stock > 0)
                {
                    opnStkDetailTypes.Add(new OpeningStockDetail()
                    {
                        Id = count++,
                        WareHouseId = stk.WareHouseId,
                        ProductId = ProductId,
                        Quantity = stk.Stock,
                        GRNDate = stk.GRNDate > new DateTime(2001, 1, 1) ? (DateTime?)stk.GRNDate : null,
                        IsPerishable = stk.IsPerishable,
                        BatchNo = stk.BatchNo?.Trim(),
                        ExpiryDate = stk.ExpiryDate?.AddDays(1).AddSeconds(-1),
                        Description = stk.Remarks,
                    });
                }
            });
            if (opnStkDetailTypes.Count > 0)
            {
                return new SqlDBAccess(CommonObj.SqlConnectionString)
                    .InsertUpdateDeleteReturnObject("[dbo].[spSetOpeningStock]", new nameValuePairs
                    {
                         
                         
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@OpeningStockDetail", opnStkDetailTypes.ToDataTable()),
                        new nameValuePair("@QueryType", "INSERT")
                    },"@OutParam"
                ).ToString();
            }
            else
            {
                return "-1";
            }
        }

        public string UploadCsv(HttpPostedFileBase httpFileBase, string ProductId)
        {
            if (httpFileBase != null)
            {
                _blobManager = new ExecuteBlobManager("MyCompany", CommonObj.NoSqlConnectionString);
                string[] Dirs = { "BulkUpload", "ProductOpeningStock", ProductId };
                return _blobManager.UploadFile(httpFileBase, Dirs, CommonObj.RequestId);
            }
            else
                return null;
        }
    }
}
