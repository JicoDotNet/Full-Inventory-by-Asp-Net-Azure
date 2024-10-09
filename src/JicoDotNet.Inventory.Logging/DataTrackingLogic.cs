using DataAccess.AzureStorage;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.Logging
{
    public class DataTrackingLogic
    {
        public static void Set(object objectValue, ICommonLogicHelper commonObj)
        {
            Task.Run(() =>
            {
                try
                {
                    DataTracking dataTracking = new DataTracking
                    {
                        PartitionKey = "MyCompany",
                        RowKey = commonObj.RequestId,
                        RequestId = commonObj.RequestId,
                        TransactionDate = GenericLogic.IstNow,
                        Data = JsonConvert.SerializeObject(objectValue)
                    };
                    // Checking that the string size is less than 64 KB.
                    if ((Encoding.UTF8.GetBytes(dataTracking.Data).Length / 1024.0D) >= 63.99D)
                        dataTracking.Data = "Length is too high";

                    ExecuteTableManager tableManager = new ExecuteTableManager("DataTracking", commonObj.NoSqlConnectionString);
                    tableManager.InsertEntityAsync(dataTracking);
                }
                catch
                {
                    // ignored
                }
            });
        }
    }
}
