using DataAccess.AzureStorage;
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
    public class TrackingLogic : ConnectionString
    {
        public TrackingLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public static async Task Log(Logger log, sCommonDto CommonObj)
        {
#pragma warning disable CS4014
            Task.Run(() =>
            {
                try
                {
                    log.PartitionKey = "ActivityLog";
                    log.RowKey = (CommonObj?.RequestId == null) ? Guid.NewGuid().ToString() : CommonObj.RequestId;

                    log.RequestId = CommonObj?.RequestId;
                    log.TransactionDate = GenericLogic.IstNow;

                    ExecuteTableManager _tableManager = new ExecuteTableManager("Log", CommonObj.NoSqlConnectionString);
                    _tableManager.InsertEntityAsync(log);
                    return;
                }
                catch
                {
                    return;
                }
            });
#pragma warning restore CS4014
        }

        public async Task LoginLog(LoginLog loginLog)
        {
#pragma warning disable CS4014
            Task.Run(() =>
            {
                try
                {
                    _tableManager = new ExecuteTableManager("Log", CommonObj.NoSqlConnectionString);
                    LoginLog log = _tableManager.RetrieveEntity<LoginLog>("RowKey eq '" + CommonObj.RequestId + "'").FirstOrDefault();
                    if (log == null)
                    {
                        log = new LoginLog
                        {
                            RowKey = CommonObj.RequestId
                        };
                    }
                    log.PartitionKey = "LoginLog";
                    log.ActivityDate = loginLog.ActivityDate;

                    _tableManager.InsertEntityAsync(log);
                }
                catch
                {
                    return;
                }
            });
#pragma warning restore CS4014            
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
