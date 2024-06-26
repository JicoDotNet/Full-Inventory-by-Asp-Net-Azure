using DataAccess.AzureStorage;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS4014

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TrackingLogic : ConnectionString
    {
        public TrackingLogic(ICommonRequestDto commonObj) : base(commonObj) { }

        public async Task Log(ILogger logData)
        {
            Task.Run(() =>
            {
                try
                {
                    Logger log = (Logger)logData;
                    log.PartitionKey = "ActivityLog";
                    log.RowKey = CommonObj?.RequestId ?? Guid.NewGuid().ToString();

                    log.RequestId = CommonObj?.RequestId;
                    log.TransactionDate = GenericLogic.IstNow;

                    ExecuteTableManager tableManager = new ExecuteTableManager("Log", CommonObj?.NoSqlConnectionString);
                    tableManager.InsertEntityAsync(log);
                }
                catch (Exception ex)
                {
                    return;
                }
            });
        }

        public async Task LoginLog(LoginLog loginLog)
        {
            Task.Run(() =>
            {
                try
                {
                    TableManager = new ExecuteTableManager("Log", CommonObj.NoSqlConnectionString);
                    LoginLog log = TableManager.RetrieveEntity<LoginLog>("RowKey eq '" + CommonObj.RequestId + "'").FirstOrDefault();
                    if (log == null)
                    {
                        log = new LoginLog
                        {
                            RowKey = CommonObj.RequestId
                        };
                    }
                    log.PartitionKey = "LoginLog";
                    log.ActivityDate = loginLog.ActivityDate;

                    TableManager.InsertEntityAsync(log);
                }
                catch
                {
                    return;
                }
            });
        }

        public List<LoginLog> GetLoginLogs(long UserId, short Limit = 5)
        {
            List<LoginLog> loginLogs =  
                new ExecuteTableManager("Log", CommonObj.NoSqlConnectionString)
                    .RetrieveEntity<LoginLog>("")
                    .OrderByDescending(a => a.ActivityDate)
                    .Take(Limit).ToList();
            return loginLogs;
        }
    }
}
