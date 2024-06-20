using DataAccess.AzureStorage;
using DataAccess.Sql;
using DataAccess.Sql.Entity;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public class ConnectionString : IConnectionString
    {
        public ConnectionString(ICommonRequestDto sCommonDtoObj)
        {
            CommonObj = sCommonDtoObj;
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
        }
        public ICommonRequestDto CommonObj { get; private set; }
        public ISqlDBAccess _sqlDBAccess { get; set; }

        protected ExecuteTableManager _tableManager;
        protected ExecuteBlobManager _blobManager;
    }
}
