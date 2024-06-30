using DataAccess.AzureStorage;
using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.Core;
using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class UserLogic : ConnectionString
    {
        public UserLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public User Get()
        {
            return new User();
        }
    }
}
