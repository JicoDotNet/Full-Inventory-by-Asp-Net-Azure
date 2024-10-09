using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Authentication.Interfaces
{
    public interface IToken : IUser
    {
        string Token { get; set; }
        DateTime? TokenDate { get; set; }
    }
}
