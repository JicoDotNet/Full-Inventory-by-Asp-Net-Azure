using DataAccess.AzureStorage;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class DataTrackingLogic
    {
        #pragma warning disable CS1998
        public static async Task Set(object _Object, sCommonDto CommonObj)
        {
            #pragma warning disable CS4014
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
                        dataTracking.Data = null;

                    ExecuteTableManager _tableManager = new ExecuteTableManager("DataTracking", CommonObj.NoSqlConnectionString);
                    _tableManager.InsertEntityAsync(dataTracking);
                }
                catch
                {
                    return;
                }
            });
            #pragma warning restore CS4014
        }
    }
}
