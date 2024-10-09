using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Net;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class LoginManagement : ConnectionString
    {
        public LoginManagement(ICommonLogicHelper commonObj) : base(commonObj) { }

        public IAccountAuthentication Authenticate(LoginCredentials loginCredentials, string requestedIP)
        {
            IAccountAuthentication accountAuthenticate = new AccountAuthentication
            {
                eLoginStatus = ELoginStatus.Error
            };

            #region Checking
            try
            {
                if (WebConfigAppSettingsAccess.UserEmail != loginCredentials.UserEmail
                    || WebConfigAppSettingsAccess.Password != loginCredentials.Password)
                {
                    accountAuthenticate.eLoginStatus = ELoginStatus.UserNameOrPasswordInvalid;
                    accountAuthenticate.credential = null;
                    return accountAuthenticate;
                }

                #region IP Permission Check
                if (!string.IsNullOrEmpty(WebConfigAppSettingsAccess.AllowedStartIP)
                    && !string.IsNullOrEmpty(WebConfigAppSettingsAccess.AllowedEndIP))
                {
                    bool IPFlag = false;
                    try
                    {
                        IPAddressRange range = new IPAddressRange(IPAddress.Parse(WebConfigAppSettingsAccess.AllowedStartIP), IPAddress.Parse(WebConfigAppSettingsAccess.AllowedEndIP));
                        IPFlag = range.IsInRange(IPAddress.Parse(requestedIP));
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
                    UserEmail = WebConfigAppSettingsAccess.UserEmail,
                    UserFullName = WebConfigAppSettingsAccess.UserFullName
                };

                // Login Success Code
                #region Token Genarate
                string _token = GenericLogic.IstNow.TimeStamp().ToString("X");
                accountAuthenticate.credential.Token = _token;
                accountAuthenticate.credential.TokenDate = GenericLogic.IstNow;
                bool IsTokenCreated = new TokenManagement(CommonObj).SetToken(accountAuthenticate.credential);
                #endregion

                // Duplicate Login check
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
            if (WebConfigAppSettingsAccess.UserEmail != UserEmail || WebConfigAppSettingsAccess.Password != Password)
                return false;
            else
                return true;
        }
    }
}