﻿using DataAccess.AzureStorage;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
#pragma warning disable CS4014
#pragma warning disable CS1998

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class DraftManagement : ConnectionString
    {
        public DraftManagement(ICommonRequestDto commonObj) : base(commonObj) { }

        public string SetAsDraft(object draftObject, EDraft draftType)
        {
            string objectId = GenericLogic.IstNow.TimeStamp().ToString("X");
            Draft draft = new Draft
            {
                PartitionKey = "MyCompany",
                RowKey = objectId,
                TransactionDate = GenericLogic.IstNow,
                IsActive = true,
                RequestId = CommonObj.RequestId,
                DraftType = draftType.ToString(),
                DraftData = JsonConvert.SerializeObject(draftObject)
            };
            TableManager = new ExecuteTableManager("Draft", CommonObj.NoSqlConnectionString);
            TableManager.InsertEntity(draft);
            return objectId;
        }

        public T GetFromDraft<T>(string objectId, EDraft draftType)
        {
            TableManager = new ExecuteTableManager("Draft", CommonObj.NoSqlConnectionString);
            string query = " RowKey eq '" + objectId + "' " +
                        " and IsActive eq true " +
                        " and DraftType eq '" + draftType + "' ";
            Draft draft = TableManager.RetrieveEntity<Draft>(query).FirstOrDefault();
            if (draft == null)
                return default;
            if (string.IsNullOrEmpty(draft.DraftData))
                return default;
            if(draft.TransactionDate.AddHours(24) < GenericLogic.IstNow)
                return default;
            return JsonConvert.DeserializeObject<T>(draft.DraftData);
        }
        
        public async Task DeleteDraft(string objectId, EDraft draftType)
        {
            
            Task.Run(() =>
            {
                TableManager = new ExecuteTableManager("Draft", CommonObj.NoSqlConnectionString);
                string query = " RowKey eq '" + objectId + "' " +
                            " and IsActive eq true " +
                            " and DraftType eq '" + draftType + "' ";
                List<Draft> drafts = TableManager.RetrieveEntity<Draft>(query);
                foreach (Draft draft in drafts)
                {
                    draft.IsActive = false;
                    TableManager.UpdateEntity(draft);
                }
            });
        }
    }
}
