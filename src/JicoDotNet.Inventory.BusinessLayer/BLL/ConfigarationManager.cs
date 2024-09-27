using DataAccess.AzureStorage;
using System;
using System.Linq;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class ConfigarationManager : ConnectionString
    {
        public ConfigarationManager(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public void SetConfig(Config config)
        {
            config.PartitionKey = "MyCompany";
            config.RowKey = Guid.NewGuid().ToString();
            config.TransactionDate = GenericLogic.IstNow;
            config.IsActive = true;

            TableManager = new ExecuteTableManager("Config", CommonObj.NoSqlConnectionString);
            TableManager.UpdateEntity(config);
        }

        public Config GetConfig()
        {
            TableManager = new ExecuteTableManager("Config", CommonObj.NoSqlConnectionString);
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
            if(config.MaxDetailsCount == 0)
                config.MaxDetailsCount = 10;

            return config;
        }
    }
}