using DataAccess.Sql;
using Newtonsoft.Json;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class LoginManagement : ConnectionString
    {
        public LoginManagement(sCommonDto CommonObj) : base(CommonObj) { }

        public AccountAuthentication Authenticate(LoginCredentials loginCredentials, string RequestedIP)
        {
            AccountAuthentication accountAuthenticate = new AccountAuthentication
            {
                eLoginStatus = ELoginStatus.Error
            };

            #region Checking
            try
            {
                if (WebConfigAccess.UserEmail != loginCredentials.UserEmail 
                    || WebConfigAccess.Password != loginCredentials.Password)
                {
                    accountAuthenticate.eLoginStatus = ELoginStatus.UserNameOrPasswordInvalid;
                    accountAuthenticate.credential = null;
                    return accountAuthenticate;
                }

                #region IP Permission Check
                if (!string.IsNullOrEmpty(WebConfigAccess.AllowedStartIP)
                    && !string.IsNullOrEmpty(WebConfigAccess.AllowedEndIP))
                {
                    bool IPFlag = false;
                    try
                    {
                        IPAddressRange range = new IPAddressRange(IPAddress.Parse(WebConfigAccess.AllowedStartIP), IPAddress.Parse(WebConfigAccess.AllowedEndIP));
                        IPFlag = range.IsInRange(IPAddress.Parse(RequestedIP));
                    }
                    catch
                    {
                        accountAuthenticate.eLoginStatus = ELoginStatus.IPAddressFormatError;
                        accountAuthenticate.credential = null;
                        return accountAuthenticate;
                    }
                    if (!IPFlag)
                    {
                        accountAuthenticate.eLoginStatus = ELoginStatus.UserNotInIPRange;
                        accountAuthenticate.credential = null;
                        return accountAuthenticate;
                    }
                }
                #endregion

                accountAuthenticate.credential = new SessionCredential
                {
                    UserEmail = WebConfigAccess.UserEmail,
                    UserFullName = WebConfigAccess.UserFullName
                };                

                // Login Success Code
                #region Token Genarate
                string _token = GenericLogic.IstNow.TimeStamp().ToString("X");
                accountAuthenticate.credential.Token = _token;
                accountAuthenticate.credential.TokenDate = GenericLogic.IstNow;
                bool IsTokenCreated = new TokenManagement(CommonObj).SetToken(accountAuthenticate.credential);
                #endregion

                // dublicate Login check
                if (!IsTokenCreated)
                {
                    accountAuthenticate.eLoginStatus = ELoginStatus.DuplicateLogin;
                    return accountAuthenticate;
                }

                // All Success
                accountAuthenticate.eLoginStatus = ELoginStatus.Success;
                return accountAuthenticate;
            }
            catch
            {
                accountAuthenticate.eLoginStatus = ELoginStatus.Error;
                accountAuthenticate.credential = null;
                return accountAuthenticate;
            }
            #endregion
        }

        public bool Authenticate(string UserEmail, string Password)
        {
            if (WebConfigAccess.UserEmail != UserEmail || WebConfigAccess.Password != Password)
                return false;
            else
                return true;
        }
    }
}