using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class.Custom;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class StockAdjustLogic : ConnectionString
    {
        public StockAdjustLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public List<StockAdjustReason> GetReasons()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetStockAdjustReason]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<StockAdjustReason>();
        }

        public string Set(StockAdjust stockAdjust)
        {
            List<StockAdjustDetailType> stockAdjustDetailTypes = new List<StockAdjustDetailType>();
            int count = 1;
            stockAdjust.StockAdjustDetails.ForEach(stockAdjustItem =>
            {
                if (stockAdjustItem.AdjustQuantity > 0)
                {
                    stockAdjustDetailTypes.Add(new StockAdjustDetailType()
                    {
                        Id = count++,
                        ProductId = stockAdjustItem.ProductId,
                        AdjustQuantity = stockAdjustItem.AdjustQuantity,
                        AvailableQuantity = stockAdjust.IsStockIncrease ? (decimal?)null : stockAdjustItem.AvailableQuantity,
                        Description = stockAdjustItem.Description,
                        GRNDate = stockAdjust.IsStockIncrease ? stockAdjustItem.GRNDate : null,
                        StockDetailId = stockAdjust.IsStockIncrease ? (long?)null : stockAdjustItem.StockDetailId,
                        IsPerishable = stockAdjustItem.IsPerishable,
                        BatchNo = stockAdjustItem.BatchNo?.Trim(),
                        ExpiryDate = stockAdjustItem.ExpiryDate?.AddDays(1).AddSeconds(-1)
                    });
                }
            });
            if (stockAdjustDetailTypes.Count > 0)
            {
                _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
                string returnString = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetStockAdjust]", new nameValuePairs
                    {
                        new nameValuePair("@StockAdjustNumber", "SKA-"),
                         
                         
                        new nameValuePair("@IsStockIncrease", stockAdjust.IsStockIncrease),
                        new nameValuePair("@AdjustReasonId", stockAdjust.AdjustReasonId),
                        new nameValuePair("@AdjustReason", stockAdjust.AdjustReason),
                        new nameValuePair("@StockAdjustDate", stockAdjust.StockAdjustDate > new DateTime(2001, 1, 1)?
                                                                (object)stockAdjust.StockAdjustDate : DBNull.Value),
                        new nameValuePair("@WareHouseId", stockAdjust.WareHouseId),
                        new nameValuePair("@Remarks", stockAdjust.Remarks),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@STDetail", stockAdjustDetailTypes.ToDataTable()),
                        new nameValuePair("@QueryType", "INSERT")
                    },
                    "@OutParam"
                ).ToString();
                return returnString;
            }
            else
            {
                return "{ \"StockAdjustId\" : -1, \"StockAdjustNumber\" : null }";
            }
        }
    }
}
