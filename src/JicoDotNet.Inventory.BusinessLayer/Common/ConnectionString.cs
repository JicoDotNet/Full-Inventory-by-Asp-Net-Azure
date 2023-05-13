using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;

namespace JicoDotNet.Inventory.BusinessLayer.Common
{
    public class ConnectionString
    {
        public ConnectionString(sCommonDto sCommonDtoObj)
        {
            CommonObj = sCommonDtoObj;
        }
        protected sCommonDto CommonObj { get; private set; }
        protected SqlDBAccess _sqlDBAccess;
        protected ExecuteTableManager _tableManager;
        protected ExecuteBlobManager _blobManager;
    }
}
