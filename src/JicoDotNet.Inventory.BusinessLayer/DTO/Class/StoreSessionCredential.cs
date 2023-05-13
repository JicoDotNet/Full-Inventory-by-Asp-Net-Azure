using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
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