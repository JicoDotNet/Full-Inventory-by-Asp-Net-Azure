using JicoDotNet.Inventory.Core.Entities;
using System;
using JicoDotNet.Authentication.Interfaces;

namespace JicoDotNet.Inventory.Core.Models
{
    public class StoreSessionCredential : IUser
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public string UserImageUrl { get; set; }

        public string Token { get; set; }
        public DateTime TokenDate { get; set; }
    }
}