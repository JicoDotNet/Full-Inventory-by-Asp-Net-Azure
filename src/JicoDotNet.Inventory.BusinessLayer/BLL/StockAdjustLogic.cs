using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class StockAdjustLogic : DBManager
    {
        public StockAdjustLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public List<StockAdjustReason> GetReasons()
        {
            return new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetStockAdjustReason]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
                }).ToList<StockAdjustReason>();
        }

        public string Set(StockAdjust stockAdjust)
        {
            List<IStockAdjustDetailType> stockAdjustDetailTypes = new List<IStockAdjustDetailType>();
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
                _sqlDBAccess = new SqlDBAccess(CommonLogicObj.SqlConnectionString);
                string returnString = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetStockAdjust]", new NameValuePairs
                    {
                        new NameValuePair("@StockAdjustNumber", "SKA-"),


                        new NameValuePair("@IsStockIncrease", stockAdjust.IsStockIncrease),
                        new NameValuePair("@AdjustReasonId", stockAdjust.AdjustReasonId),
                        new NameValuePair("@AdjustReason", stockAdjust.AdjustReason),
                        new NameValuePair("@StockAdjustDate", stockAdjust.StockAdjustDate > new DateTime(2001, 1, 1)?
                                                                (object)stockAdjust.StockAdjustDate : DBNull.Value),
                        new NameValuePair("@WareHouseId", stockAdjust.WareHouseId),
                        new NameValuePair("@Remarks", stockAdjust.Remarks),
                        new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                        new NameValuePair("@STDetail", stockAdjustDetailTypes.ToDataTable()),
                        new NameValuePair("@QueryType", "INSERT")
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
