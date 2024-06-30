using DataAccess.AzureStorage;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

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