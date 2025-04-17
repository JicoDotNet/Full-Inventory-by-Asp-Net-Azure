using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Common;
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

            TableManager = new AzureTableAccess("Config", CommonLogicObj.NoSqlConnectionString);
            TableManager.UpdateEntity(config);
        }

        public Config GetConfig()
        {
            TableManager = new AzureTableAccess("Config", CommonLogicObj.NoSqlConnectionString);
            Config config = TableManager.RetrieveEntity<Config>("");
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