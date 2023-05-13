using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class SessionCredential : TableEntity, IUser
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }

        public string Token { get; set; }
        public DateTime TokenDate { get; set; }
    }

    public class SessionToken : IUser
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public DateTime TokenDate { get; set; }

        public SessionToken(SessionCredential credential)
        {
            this.UserFullName = credential.UserFullName;
            this.UserEmail = credential.UserEmail;
            this.Token = credential.Token;
            this.TokenDate = credential.TokenDate;
        }
    }
}