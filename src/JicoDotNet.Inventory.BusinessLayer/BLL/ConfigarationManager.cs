using DataAccess.AzureStorage;
using JicoDotNet.Validator.Interfaces;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Validator.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class ConfigarationManager : DBManager
    {
        public ConfigarationManager(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public void SetConfig(Config config)
        {
            config.PartitionKey = "MyCompany";
            config.RowKey = Guid.NewGuid().ToString();
            config.TransactionDate = GenericLogic.IstNow;
            config.IsActive = true;

            TableManager = new ExecuteTableManager("Config", CommonLogicObj.NoSqlConnectionString);
            TableManager.UpdateEntity(config);
        }

        public Config GetConfig()
        {
            TableManager = new ExecuteTableManager("Config", CommonLogicObj.NoSqlConnectionString);
            Config config = TableManager.RetrieveEntity<Config>("").FirstOrDefault();
            if (config == null)
            {
                config = new Config
                {
                    TimeZone = 5.5,
                    CurrencySymbol = "₹",
                    CurrencyCode = "INR",
                    MaxDetailsCount = 10,
                    IsActive = true
                };
            }
            if (config.MaxDetailsCount == 0)
                config.MaxDetailsCount = 10;

            return config;
        }
    }
}