using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Authentication.Interfaces
{
    public interface IHttpRequest
    {
        string RequestId { get; set; }
    }
}
