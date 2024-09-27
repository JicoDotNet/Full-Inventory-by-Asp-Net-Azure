using System;

namespace JicoDotNet.Inventory.Core.Entities
{
    public interface IToken : IUser
    {
        string Token { get; set; }
        DateTime? TokenDate { get; set; }
    }
}
