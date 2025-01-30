using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class UserLogic : DBManager
    {
        public UserLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        public User Get()
        {
            return new User();
        }
    }
}
