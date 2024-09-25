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

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class StockTransferLogic : ConnectionString
    {
        public StockTransferLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public string Set(StockTransfer stockTransfer)
        {
            List<StockTransferDetailType> stDetailTypes = new List<StockTransferDetailType>();
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
                    .InsertUpdateDeleteReturnObject("[dbo].[spSetStockTransfer]", new nameValuePairs
                    {
                         
                         
                        new nameValuePair("@FromWareHouseId", stockTransfer.FromWareHouseId),
                        new nameValuePair("@ToWareHouseId", stockTransfer.ToWareHouseId),
                        new nameValuePair("@StockTransferNumber", "SKT-"),
                        new nameValuePair("@StockTransferDate", stockTransfer.StockTransferDate),
                        new nameValuePair("@Remarks", stockTransfer.Remarks),
                        new nameValuePair("@RequestId", CommonObj.RequestId),
                        new nameValuePair("@STDetail", stDetailTypes.ToDataTable()),
                        new nameValuePair("@QueryType", "INSERT")
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
