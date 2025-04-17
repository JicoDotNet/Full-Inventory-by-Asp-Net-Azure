using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Common.Auth;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Net;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class LoginManagement : DBManager
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
                if (CommonLogicObj.AppSettings.UserEmail != loginCredentials.UserEmail
                    || CommonLogicObj.AppSettings.Password != loginCredentials.Password)
                {
                    accountAuthenticate.eLoginStatus = ELoginStatus.UserNameOrPasswordInvalid;
                    accountAuthenticate.credential = null;
                    return accountAuthenticate;
                }

                #region IP Permission Check
                if (!string.IsNullOrEmpty(CommonLogicObj.AppSettings.AllowedStartIP)
                    && !string.IsNullOrEmpty(CommonLogicObj.AppSettings.AllowedEndIP))
                {
                    bool IPFlag = false;
                    try
                    {
                        IPAddressRange range = new IPAddressRange(IPAddress.Parse(CommonLogicObj.AppSettings.AllowedStartIP), IPAddress.Parse(CommonLogicObj.AppSettings.AllowedEndIP));
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
                    UserEmail = CommonLogicObj.AppSettings.UserEmail,
                    UserFullName = CommonLogicObj.AppSettings.UserFullName
                };

                // Login Success Code
                #region Token Genarate
                string _token = GenericLogic.IstNow.TimeStamp().ToString("X");
                accountAuthenticate.credential.Token = _token;
                accountAuthenticate.credential.TokenDate = GenericLogic.IstNow;
                bool IsTokenCreated = new TokenManager(CommonLogicObj).SetToken(accountAuthenticate.credential);
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
            if (CommonLogicObj.AppSettings.UserEmail != UserEmail || CommonLogicObj.AppSettings.Password != Password)
                return false;
            else
                return true;
        }
    }
}