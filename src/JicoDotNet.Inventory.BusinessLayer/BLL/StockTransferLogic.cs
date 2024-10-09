using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Custom;
using JicoDotNet.Inventory.Core.Custom.Interface;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class StockTransferLogic : ConnectionString
    {
        public StockTransferLogic(ICommonLogicHelper commonObj) : base(commonObj) { }

        public string Set(StockTransfer stockTransfer)
        {
            List<IStockTransferDetailType> stDetailTypes = new List<IStockTransferDetailType>();
            int count = 1;
            if (stockTransfer.StockTransferDetails != null)
            {
                stockTransfer.StockTransferDetails.ForEach(std =>
                {
                    if (std.TransferQuantity > 0)
                    {
                        stDetailTypes.Add(new StockTransferDetailType()
                        {
                            Id = count++,
                            ProductId = std.ProductId,
                            AvailableQuantity = std.AvailableQuantity,
                            TransferQuantity = std.TransferQuantity,
                            Description = std.Description,
                            StockDetailId = std.StockDetailId,
                            IsPerishable = std.IsPerishable,
                            ExpiryDate = std.ExpiryDate?.AddDays(1).AddSeconds(-1),
                            BatchNo = std.BatchNo?.Trim()
                        });
                    }
                });
            }
            if (stDetailTypes.Count > 0)
            {
                return new SqlDBAccess(CommonObj.SqlConnectionString)
                    .DataManipulation(GenericLogic.SqlSchema + ".[spSetStockTransfer]", new NameValuePairs
                    {


                        new NameValuePair("@FromWareHouseId", stockTransfer.FromWareHouseId),
                        new NameValuePair("@ToWareHouseId", stockTransfer.ToWareHouseId),
                        new NameValuePair("@StockTransferNumber", "SKT-"),
                        new NameValuePair("@StockTransferDate", stockTransfer.StockTransferDate),
                        new NameValuePair("@Remarks", stockTransfer.Remarks),
                        new NameValuePair("@RequestId", CommonObj.RequestId),
                        new NameValuePair("@STDetail", stDetailTypes.ToDataTable()),
                        new NameValuePair("@QueryType", "INSERT")
                    }, "@OutParam"
                ).ToString();
            }
            else
            {
                return "{\"StockTransferId\": -1 }";
            }
        }
    }
}
