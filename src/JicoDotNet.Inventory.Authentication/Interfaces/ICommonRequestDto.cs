using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Authentication.Interfaces
{
    public interface ICommonRequestDto : IHttpRequest
    {
        string Token { get; set; }
        string SqlConnectionString { get; set; }
        object NoSqlConnectionString { get; set; }
    }
}
