using DataAccess.Sql;
using DataAccess.AzureStorage;
using DataAccess.Sql.Entity;
using JicoDotNet.Authentication.Entities;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Common
{
    public abstract class DBManager : DBManagerHelper, IDBManager
    {
        protected DBManager(ICommonLogicHelper commonDtoObj) : base(commonDtoObj)
        {
            _sqlDBAccess = new SqlDBAccess(commonDtoObj.SqlConnectionString);
        }
        public ISqlDBAccess _sqlDBAccess { get; set; }

        protected ExecuteTableManager TableManager;
        protected ExecuteBlobManager BlobManager;
    }
}
