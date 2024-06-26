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
    public class DraftManagment : ConnectionString
    {
        public DraftManagment(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public string SetAsDraft(object draftObject, EDraft DraftType)
        {
            string ObjectId;
            try
            {
                ObjectId = GenericLogic.IstNow.TimeStamp().ToString("X");
                Draft draft = new Draft
                {
                    PartitionKey = "MyCompany",
                    RowKey = ObjectId,
                    TransactionDate = GenericLogic.IstNow,
                    IsActive = true,
                    RequestId = CommonObj.RequestId,
                    DraftType = DraftType.ToString(),
                    DraftData = JsonConvert.SerializeObject(draftObject)
                };
                TableManager = new ExecuteTableManager("Draft", CommonObj.NoSqlConnectionString);
                TableManager.InsertEntity(draft);
            }
            catch (Exception)
            {
                throw;
            }
            return ObjectId;
        }

        public T GetFromDraft<T>(string ObjectId, EDraft DraftType)
        {
            TableManager = new ExecuteTableManager("Draft", CommonObj.NoSqlConnectionString);
            string Qry = " RowKey eq '" + ObjectId + "' " +
                        " and IsActive eq true " +
                        " and DraftType eq '" + DraftType.ToString() + "' ";
            Draft draft = TableManager.RetrieveEntity<Draft>(Qry).FirstOrDefault();
            if (draft == null)
                return default(T);
            if (string.IsNullOrEmpty(draft.DraftData))
                return default(T);
            if(draft.TransactionDate.AddHours(24) < GenericLogic.IstNow)
                return default(T);
            return JsonConvert.DeserializeObject<T>(draft.DraftData);
        }

        #pragma warning disable CS1998
        public async Task DeleteDraft(string ObjectId, EDraft DraftType)
        {
            #pragma warning disable CS4014
            Task.Run(() =>
            {
                TableManager = new ExecuteTableManager("Draft", CommonObj.NoSqlConnectionString);
                string Qry = " RowKey eq '" + ObjectId + "' " +
                            " and IsActive eq true " +
                            " and DraftType eq '" + DraftType.ToString() + "' ";
                List<Draft> drafts = TableManager.RetrieveEntity<Draft>(Qry);
                foreach (Draft draft in drafts)
                {
                    draft.IsActive = false;
                    TableManager.InsertEntity(draft, false);
                }
            });
            #pragma warning restore CS4014
        }
    }
}
