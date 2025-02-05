using DataAccess.Sql;
using DataAccess.AzureStorage;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.Core.Entities;
using System;

namespace JicoDotNet.Inventory.Core.Common
{
    public abstract class DBManager : IDBManagerHelper
    {        
        protected DBManager(ICommonLogicHelper commonDtoObj)
        {
            CommonLogicObj = commonDtoObj ?? throw new ArgumentNullException(nameof(commonDtoObj), "Object can not be null");
            
            _sqlDBAccess = new SqlDBAccess(commonDtoObj.SqlConnectionString);
        }

        public ICommonLogicHelper CommonLogicObj { get; private set; }
        public ISqlDBAccess _sqlDBAccess { get; private set; }

        protected ExecuteTableManager TableManager;
        protected ExecuteBlobManager BlobManager;
    }
}
