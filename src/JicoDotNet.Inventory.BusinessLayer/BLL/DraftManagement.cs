using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class DraftManagement : DBManager
    {
        public DraftManagement(ICommonLogicHelper commonObj) : base(commonObj) { }

        public string SetAsDraft(object draftObject, EDraft draftType)
        {
            string objectId = GenericLogic.IstNow.TimeStamp().ToString("X");
            Draft draft = new Draft
            {
                PartitionKey = "MyCompany",
                RowKey = objectId,
                TransactionDate = GenericLogic.IstNow,
                IsActive = true,
                RequestId = CommonLogicObj.RequestId,
                DraftType = draftType.ToString(),
                DraftData = JsonConvert.SerializeObject(draftObject)
            };
            TableManager = new AzureTableAccess("Draft", CommonLogicObj.NoSqlConnectionString);
            TableManager.InsertEntity(draft);
            return objectId;
        }

        public T GetFromDraft<T>(string objectId, EDraft draftType)
        {
            TableManager = new AzureTableAccess("Draft", CommonLogicObj.NoSqlConnectionString);
            string query = " RowKey eq '" + objectId + "' " +
                        " and IsActive eq true " +
                        " and DraftType eq '" + draftType + "' ";
            Draft draft = TableManager.RetrieveEntity<Draft>(query);
            if (draft == null)
                return default;
            if (string.IsNullOrEmpty(draft.DraftData))
                return default;
            if (draft.TransactionDate.AddHours(24) < GenericLogic.IstNow)
                return default;
            return JsonConvert.DeserializeObject<T>(draft.DraftData);
        }

        public void DeleteDraft(string objectId, EDraft draftType)
        {
            Task.Run(() =>
            {
                TableManager = new AzureTableAccess("Draft", CommonLogicObj.NoSqlConnectionString);
                string query = " RowKey eq '" + objectId + "' " +
                            " and IsActive eq true " +
                            " and DraftType eq '" + draftType + "' ";
                List<Draft> drafts = TableManager.RetrieveEntities<Draft>(query);
                foreach (Draft draft in drafts)
                {
                    draft.IsActive = false;
                    TableManager.UpdateEntity(draft);
                }
            });
        }
    }
}
