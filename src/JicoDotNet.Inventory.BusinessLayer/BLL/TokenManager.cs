using DataAccess.AzureStorage;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using JicoDotNet.Inventory.Core.Entities;

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

        public bool IsValid(string token, string email)
        {
            TableManager = new ExecuteTableManager("SessionToken", CommonLogicObj.NoSqlConnectionString);
            List<SessionCredential> credentials = TableManager.RetrieveEntity<SessionCredential>(
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