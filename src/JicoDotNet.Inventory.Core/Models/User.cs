using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Authentication.Interfaces;

namespace JicoDotNet.Inventory.Core.Models
{
    public class User : IUser
    {
        public string UserEmail { get; set; }
        public string UserFullName { get; set; }
    }
}