using System;

namespace JicoDotNet.Inventory.Core.Entities.Inner
{
    public interface IToken : IUserCore
    {
        string Token { get; set; }
        DateTime? TokenDate { get; set; }
    }
}
