using DataAccess.AzureStorage;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class SMSBankLogic : DBManager
    {
        public SMSBankLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public bool Deduction()
        {
            string Query = " IsActive eq true";
            TableManager = new ExecuteTableManager("SMSBank", CommonLogicObj.NoSqlConnectionString);
            SMSBank sMSBank = TableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
            if (sMSBank != null && sMSBank.Balance > 0)
            {
                sMSBank.Balance--;
                TableManager.UpdateEntity(sMSBank);
                return true;
            }
            return false;
        }

        public void Addition(long Balance)
        {
            Task.Run(() =>
            {
                string Query = " IsActive eq true";
                TableManager = new ExecuteTableManager("SMSBank", CommonLogicObj.NoSqlConnectionString);
                SMSBank sMSBank = TableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
                if (sMSBank != null)
                {
                    sMSBank.Balance += Balance;
                    TableManager.ReplaceEntity(sMSBank);
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
                        RequestId = CommonLogicObj.RequestId,
                        TransactionDate = GenericLogic.IstNow
                    };
                    TableManager.InsertEntity(sMSBank);
                    return;
                }
            });
        }

        public SMSBank Get()
        {
            string Query = " IsActive eq true";
            TableManager = new ExecuteTableManager("SMSBank", CommonLogicObj.NoSqlConnectionString);
            return TableManager.RetrieveEntity<SMSBank>(Query).FirstOrDefault();
        }
    }
}
