using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.Logging
{
    public class AuditLogLogic
    {
        public static void AuditLog(ILogger logData, Core.Entities.ICommonLogicHelper commonObj)
        {
            Task.Run(() =>
            {
                try
                {
                    Logger log = (Logger)logData;
                    log.PartitionKey = "ActivityLog";
                    log.RowKey = commonObj?.RequestId ?? Guid.NewGuid().ToString();

                    log.RequestId = commonObj?.RequestId;
                    log.TransactionDate = GenericLogic.IstNow;

                    IAzureTableAccess tableManager = new AzureTableAccess("Log", commonObj?.NoSqlConnectionString);
                    tableManager.InsertEntityAsync(log);
                }
                catch
                {
                    // ignored
                }
            });
        }

        public static void LoginLog(Core.Entities.ICommonLogicHelper commonObj)
        {
            Task.Run(() =>
            {
                try
                {
                    IAzureTableAccess tableManager = new AzureTableAccess("Log", commonObj.NoSqlConnectionString);
                    LoginLog log = tableManager.RetrieveEntity<LoginLog>("RowKey eq '" + commonObj.RequestId + "'") ??
                                   new LoginLog
                                   {
                                       ActivityDate = GenericLogic.IstNow,
                                       RowKey = commonObj.RequestId
                                   };
                    log.PartitionKey = "LoginLog";
                    log.ActivityDate = GenericLogic.IstNow;

                    tableManager.InsertEntityAsync(log);
                }
                catch
                {
                    // ignored
                }
            });
        }
    }
}
