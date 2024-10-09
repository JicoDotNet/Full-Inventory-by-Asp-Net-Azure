using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class StockLogic : ConnectionString
    {
        public StockLogic(ICommonRequestDto commonObj) : base(commonObj) { }

        public List<Stock> Get(Stock stock)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            NameValuePairs nvp = new NameValuePairs()
            {



                new NameValuePair("@WareHouseId", stock.WareHouseId),
                new NameValuePair("@ProductId", stock.ProductId),
                new NameValuePair("@GRNOrShipmentDate", stock.GRNOrShipmentDate > new DateTime(2001, 1, 1)?
                                                           (object)stock.GRNOrShipmentDate : DBNull.Value),
                new NameValuePair("@QueryType", "CURRENT")
            };
            return _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetStock]", nvp).ToList<Stock>();
        }

        public List<Stock> GetDetail(Stock stock)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            NameValuePairs nvp = new NameValuePairs()
            {



                new NameValuePair("@WareHouseId", stock.WareHouseId),
                new NameValuePair("@ProductId", stock.ProductId),

                new NameValuePair("@QueryType", "DETAIL")
            };
            DataSet dataSet = _sqlDBAccess.GetDataSet(GenericLogic.SqlSchema + ".[spGetStock]", nvp);
            List<Stock> stocks = dataSet.Tables[0].ToList<Stock>();
            List<StockDetail> stockDetails = dataSet.Tables[1].ToList<StockDetail>();

            foreach (Stock loopstock in stocks)
            {
                loopstock.StockDetails = stockDetails
                                            .Where(a => a.WareHouseId == loopstock.WareHouseId
                                                && a.ProductId == loopstock.ProductId).ToList();
            }
            return stocks;
        }

        public long TotalNonOpeningStockQuantity(long productId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            NameValuePairs nvp = new NameValuePairs()
            {



                new NameValuePair("@ProductId", productId),
                new NameValuePair("@QueryType", "NOTOPNINGSTOCK")
            };
            try
            {
                DataTable dt = _sqlDBAccess.GetData(GenericLogic.SqlSchema + ".[spGetStock]", nvp);
                if (dt != null)
                    if (dt.Rows.Count > 0)
                        if (dt.Rows[0]["ProductQuantity"] != null)
                            return Convert.ToInt64(dt.Rows[0]["ProductQuantity"].ToString());
            }
            catch
            {
                // ignored
            }

            return 0;
        }

        public string AddOpeningStock(List<StockDetail> stockDetails, long productId)
        {
            List<IOpeningStockDetail> opnStkDetailTypes = new List<IOpeningStockDetail>();
            int count = 1;
            stockDetails.ForEach(stk =>
            {
                if (stk.Stock > 0)
                {
                    opnStkDetailTypes.Add(new OpeningStockDetail
                    {
                        Id = count++,
                        WareHouseId = stk.WareHouseId,
                        ProductId = productId,
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
                    .DataManipulation(GenericLogic.SqlSchema + ".[spSetOpeningStock]", new NameValuePairs
                    {


                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@OpeningStockDetail", opnStkDetailTypes.ToDataTable()),
                        new NameValuePair("@QueryType", "INSERT")
                    }, "@OutParam"
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
                BlobManager = new ExecuteBlobManager("MyCompany", CommonObj.NoSqlConnectionString);
                string[] Dirs = { "BulkUpload", "ProductOpeningStock", ProductId };
                return BlobManager.UploadFile(httpFileBase, Dirs, CommonObj.RequestId);
            }
            else
                return null;
        }
    }
}
