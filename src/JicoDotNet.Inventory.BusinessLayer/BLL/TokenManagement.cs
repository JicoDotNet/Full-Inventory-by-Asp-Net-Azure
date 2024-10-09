using DataAccess.AzureStorage;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TokenManagement : DBManager
    {
        public TokenManagement(ICommonLogicHelper CommonObj) : base(CommonObj) { }

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
                ExecuteTableManager tableManager = new ExecuteTableManager("SessionToken", CommonLogicObj.NoSqlConnectionString);
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

        public ISessionCredential GetCredential(string token)
        {
            TableManager = new ExecuteTableManager("SessionToken", CommonLogicObj.NoSqlConnectionString);
            List<SessionCredential> credentials = TableManager.RetrieveEntity<SessionCredential>("Token eq '" + token + "'");
            if (credentials.Count == 1)
                return credentials[0];
            return null;
        }

        /// <summary>
        /// For Previous Session - Duplicate Session
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public IList<SessionCredential> GetUser(string userEmail)
        {
            ExecuteTableManager tableManager = new ExecuteTableManager("SessionToken", CommonLogicObj.NoSqlConnectionString);
            IList<SessionCredential> credentials = tableManager
                .RetrieveEntity<SessionCredential>("UserEmail eq '" + userEmail + "'");
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
                TableManager = new ExecuteTableManager("SessionToken", CommonLogicObj.NoSqlConnectionString);
                List<SessionCredential> credentials = TableManager.RetrieveEntity<SessionCredential>("UserEmail eq '" + UserEmail + "'");
                foreach (SessionCredential sc in credentials)
                {
                    TableManager.DeleteEntity(sc);
                }
            });
        }
    }
}