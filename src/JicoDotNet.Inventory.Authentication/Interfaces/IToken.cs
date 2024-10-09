using System;

namespace JicoDotNet.Authentication.Interfaces
{
    public interface IToken : IUser
    {
        string Token { get; set; }
        DateTime? TokenDate { get; set; }
    }
}
