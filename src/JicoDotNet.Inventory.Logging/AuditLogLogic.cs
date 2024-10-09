using DataAccess.AzureStorage;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using JicoDotNet.Validator.Interfaces;

namespace JicoDotNet.Inventory.Logging
{
    public class AuditLogLogic
    {
        public static void AuditLog(ILogger logData, ICommonLogicHelper commonObj)
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

                    ExecuteTableManager tableManager = new ExecuteTableManager("Log", commonObj?.NoSqlConnectionString);
                    tableManager.InsertEntityAsync(log);
                }
                catch
                {
                    // ignored
                }
            });
        }

        public static void LoginLog(ICommonLogicHelper commonObj)
        {
            Task.Run(() =>
            {
                try
                {
                    ExecuteTableManager tableManager = new ExecuteTableManager("Log", commonObj.NoSqlConnectionString);
                    LoginLog log = tableManager.RetrieveEntity<LoginLog>("RowKey eq '" + commonObj.RequestId + "'")
                                       .FirstOrDefault() ??
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
