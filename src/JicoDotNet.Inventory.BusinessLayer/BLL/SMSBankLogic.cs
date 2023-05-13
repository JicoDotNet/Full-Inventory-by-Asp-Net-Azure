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
    public class SMSBankLogic : ConnectionString
    {
        public SMSBankLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public bool Deduction()
        {
            string Query = " IsActive eq true";
            _tableManager = new ExecuteTableManager("SMSBank", CommonObj.NoSqlConnectionString);
            SMSBank sMSBank = _tableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
            if (sMSBank != null && sMSBank.Balance > 0)
            {
                sMSBank.Balance--;
                _tableManager.UpdateEntity(sMSBank);
                return true;
            }
            return false;
        }

        public async Task Addition(long Balance)
        {
            #pragma warning disable CS4014
            Task.Run(() =>
            {
                string Query = " IsActive eq true";
                _tableManager = new ExecuteTableManager("SMSBank", CommonObj.NoSqlConnectionString);
                SMSBank sMSBank = _tableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
                if (sMSBank != null)
                {
                    sMSBank.Balance += Balance;
                    _tableManager.ReplaceEntity(sMSBank);
                    return;
                }
                else
                {
                    sMSBank = new SMSBank
                    {
                        PartitionKey = "MyCompany",
                        RowKey = GenericLogic.IstNow.TimeStamp().ToString("X"),
                        Balance = Balance,
                        IsActive = true,
                        RequestId = CommonObj.RequestId,
                        TransactionDate = GenericLogic.IstNow
                    };
                    _tableManager.InsertEntity(sMSBank);
                    return;
                }
            });
            #pragma warning restore CS4014
        }

        public SMSBank Get()
        {
            string Query = " IsActive eq true";
            _tableManager = new ExecuteTableManager("SMSBank", CommonObj.NoSqlConnectionString);
            return _tableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
        }
    }
}
