using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TokenManager : DBManager
    {
        public TokenManager(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        /// <summary>
        /// Token Creation
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>
        /// true: if Token Creation successfull... false: if already exists the token for a user
        /// </returns>
        public bool SetToken(ISessionCredential credential)
        {
            try
            {
                IAzureTableAccess tableManager = new AzureTableAccess("SessionToken", CommonLogicObj.NoSqlConnectionString);
                SessionCredential sessionCredential = credential as SessionCredential;
                sessionCredential.PartitionKey = "MyCompany";
                sessionCredential.RowKey = credential.UserEmail;
                tableManager.InsertEntity(sessionCredential);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValid(string token, string email)
        {
            TableManager = new AzureTableAccess("SessionToken", CommonLogicObj.NoSqlConnectionString);
            List<SessionCredential> credentials = TableManager.RetrieveEntities<SessionCredential>(
                                        "Token eq '" + token + "' " +
                                        "and UserEmail eq '" + email + "'");
            if (credentials.Count == 1)
                return true;
            return false;
        }

        /// <summary>
        /// For Previous Session - Duplicate Session
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public IList<SessionCredential> GetUser(string userEmail)
        {
            IAzureTableAccess tableManager = new AzureTableAccess("SessionToken", CommonLogicObj.NoSqlConnectionString);
            IList<SessionCredential> credentials = tableManager
                .RetrieveEntities<SessionCredential>("UserEmail eq '" + userEmail + "'");
            foreach (ISessionCredential sessionCredential in credentials)
            {
                sessionCredential.Token = null;
            }
            return credentials;
        }

        public void Delete(string UserEmail)
        {
            Task.Run(() =>
            {
                TableManager = new AzureTableAccess("SessionToken", CommonLogicObj.NoSqlConnectionString);
                List<SessionCredential> credentials = TableManager.RetrieveEntities<SessionCredential>("UserEmail eq '" + UserEmail + "'");
                foreach (SessionCredential sc in credentials)
                {
                    TableManager.DeleteEntity(sc);
                }
            });
        }
    }
}