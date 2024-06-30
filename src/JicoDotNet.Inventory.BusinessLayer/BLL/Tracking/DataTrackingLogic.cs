using System;
using System.Threading.Tasks;
using DataAccess.AzureStorage;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using Newtonsoft.Json;
#pragma warning disable CS4014
#pragma warning disable CS1998

namespace JicoDotNet.Inventory.BusinessLayer.BLL.Tracking
{
    public class DataTrackingLogic
    {
        public static async Task Set(object _Object, ICommonRequestDto CommonObj)
        {
            Task.Run(() =>
            {
                try
                {
                    DataTracking dataTracking = new DataTracking
                    {
                        PartitionKey = "MyCompany",
                        RowKey = CommonObj.RequestId,
                        RequestId = CommonObj.RequestId,
                        TransactionDate = GenericLogic.IstNow,
                        Data = JsonConvert.SerializeObject(_Object)
                    };
                    if (dataTracking.Data.Length >= 32000)
                        dataTracking.Data = "Length is too high";

                    ExecuteTableManager _tableManager = new ExecuteTableManager("DataTracking", CommonObj.NoSqlConnectionString);
                    _tableManager.InsertEntityAsync(dataTracking);
                }
                catch
                {
                    return;
                }
            });
        }
    }
}
