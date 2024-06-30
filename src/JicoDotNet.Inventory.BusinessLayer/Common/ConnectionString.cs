using System;
using DataAccess.AzureStorage;
using DataAccess.Sql;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public abstract class ConnectionString : IConnectionString
    {
        protected ConnectionString(ICommonRequestDto commonDtoObj)
        {
            CommonObj = commonDtoObj ?? throw new ArgumentNullException(nameof(commonDtoObj), "Object can not be null");
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
        }
        public ICommonRequestDto CommonObj { get; }
        public ISqlDBAccess _sqlDBAccess { get; set; }

        protected ExecuteTableManager TableManager;
        protected ExecuteBlobManager BlobManager;
    }
}
