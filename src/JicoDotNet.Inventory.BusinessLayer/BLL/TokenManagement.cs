using DataAccess.AzureStorage;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class TokenManagement : ConnectionString
    {
        public TokenManagement(ICommonRequestDto CommonObj) : base(CommonObj) { }

        /// <summary>
        /// Token Creation
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>
        /// true: if Token Creation successfull... false: if already exists the token for a user
        /// </returns>
        public bool SetToken(SessionCredential credential)
        {
            try
            {
                ExecuteTableManager tableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
                credential.PartitionKey = "MyCompany";
                credential.RowKey = credential.UserEmail.ToString();
                tableManager.InsertEntity(credential);
                return true;
            }
            catch
            {
                return false;
            }
        }

//        public async Task UpdateToken(User user, ETokenChangePurpose purpose)
//        {
//#pragma warning disable CS4014
//            Task.Run(() =>
//            {
//                ExecuteTableManager tableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
//                string Query = "PartitionKey eq '" + CommonObj.TenantCode + "' and UserId eq " + user.UserId + "";
//                List<SessionCredential> credentials = tableManager.RetrieveEntity<SessionCredential>(Query);
//                if (credentials.Count == 1)
//                {
//                    SessionCredential credential = credentials[0];
//                    if (!string.IsNullOrEmpty(user.UserImageUrl) && purpose == ETokenChangePurpose.Image)
//                    {
//                        credential.UserImageUrl = user.UserImageUrl;
//                    }
//                    else if (purpose == ETokenChangePurpose.Profile)
//                    {
//                        credential.UserEmail = user.UserEmail;
//                        credential.UserFullName = user.UserFullName;
//                        credential.UserMobile = user.UserMobile;
//                        credential.UserTypeId = user.UserTypeId;
//                    }
//                    else if (purpose == ETokenChangePurpose.Admin)
//                    {
//                        credential.IsAdmin = user.IsAdmin;
//                    }
//                    else if (purpose == ETokenChangePurpose.Menu)
//                    {
//                        // -- Permission Code
//                        credential.Menu = JsonConvert.SerializeObject(new MenuLogic(CommonObj)
//                            .GetUserMenuGroup(user.UserId, CommonObj.TenantId));
//                    }
//                    tableManager.InsertEntity(credential, false);
//                }
//            });
//#pragma warning restore CS4014
//        }

        public SessionCredential GetCredential(string Token)
        {
            TableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
            List<SessionCredential> credentials = TableManager.RetrieveEntity<SessionCredential>("Token eq '" + Token + "'");
            if (credentials.Count == 1)
                return credentials[0];
            return null;
        }

        //private List<MenuGroup> GetMenu(long UserId, string TenantCode)
        //{
        //    ExecuteTableManager tableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
        //    List<SessionCredential> credentials = tableManager.RetrieveEntity<SessionCredential>("PartitionKey eq '" + TenantCode + "' and UserId eq " + UserId + "");
        //    if (credentials.Count == 1)
        //        return (JsonConvert.DeserializeObject<List<MenuGroup>>(credentials[0].Menu ?? "")) ?? new List<MenuGroup>();
        //    return null;
        //}

        //public List<MenuGroup> GetMenuNormal(long UserId, string TenantCode)
        //{
        //    List<MenuGroup> menuGroups = GetMenu(UserId, TenantCode);
        //    foreach (MenuGroup group in menuGroups.ToList())
        //    {
        //        group.MenuLists = group.MenuLists.Where(a => !a.IsReport).ToList();
        //        if (group.MenuLists.Count == 0)
        //            menuGroups.Remove(group);
        //    }
        //    return menuGroups;
        //}

        //public List<MenuGroup> GetMenuReport(long UserId, string TenantCode)
        //{
        //    List<MenuGroup> menuGroups = GetMenu(UserId, TenantCode);
        //    foreach (MenuGroup group in menuGroups.ToList())
        //    {
        //        group.MenuLists = group.MenuLists.Where(a => a.IsReport).ToList();
        //        if (group.MenuLists.Count == 0)
        //            menuGroups.Remove(group);
        //    }
        //    return menuGroups;
        //}


        /// <summary>
        /// For Previous Session - Duplicate Session
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<SessionCredential> GetUser(string UserEmail)
        {
            ExecuteTableManager tableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
            List<SessionCredential> credentials = tableManager
                .RetrieveEntity<SessionCredential>("UserEmail eq '" + UserEmail + "'");
            foreach (SessionCredential sessionCredential in credentials)
            {
                sessionCredential.Token = null;
            }
            return credentials;
        }

        public async Task Delete(string UserEmail)
        {
#pragma warning disable CS4014
            Task.Run(() =>
            {
                TableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
                List<SessionCredential> credentials = TableManager.RetrieveEntity<SessionCredential>("UserEmail eq '" + UserEmail + "'");
                foreach (SessionCredential sc in credentials)
                {
                    TableManager.DeleteEntity(sc);
                }
            });
#pragma warning restore CS4014
        }

        //        public async Task Delete(bool ExceptSelf = true)
        //        {
        //#pragma warning disable CS4014
        //            Task.Run(() =>
        //            {
        //                _tableManager = new ExecuteTableManager("SessionToken", CommonObj.NoSqlConnectionString);
        //                List<SessionCredential> credentials = _tableManager.RetrieveEntity<SessionCredential>("PartitionKey eq '" + CommonObj.TenantCode + "'");
        //                foreach (SessionCredential sc in credentials)
        //                {
        //                    if (ExceptSelf)
        //                    {
        //                        if (CommonObj.UserId != sc.UserId)
        //                            _tableManager.DeleteEntity(sc);
        //                    }
        //                    else
        //                    {
        //                        _tableManager.DeleteEntity(sc);
        //                    }
        //                }
        //            });
        //#pragma warning restore CS4014
        //        }
    }
}
