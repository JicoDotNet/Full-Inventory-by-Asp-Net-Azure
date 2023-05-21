using DataAccess.AzureStorage;
using Newtonsoft.Json;
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
    public class ConfigarationManager : ConnectionString
    {
        public ConfigarationManager(sCommonDto CommonObj) : base(CommonObj) { }

        public void SetConfig(Config config)
        {
            config.PartitionKey = "MyCompany";
            config.RowKey = Guid.NewGuid().ToString();
            config.TransactionDate = GenericLogic.IstNow;
            config.IsActive = true;

            _tableManager = new ExecuteTableManager("Config", CommonObj.NoSqlConnectionString);
            _tableManager.UpdateEntity(config);
        }

        public Config GetConfig()
        {
            _tableManager = new ExecuteTableManager("Config", CommonObj.NoSqlConnectionString);
            Config config = _tableManager.RetrieveEntity<Config>("").FirstOrDefault();
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