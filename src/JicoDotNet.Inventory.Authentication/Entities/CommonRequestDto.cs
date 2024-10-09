using JicoDotNet.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Authentication.Entities
{
    public  class CommonRequestDto : ICommonRequestDto
    {
        public string RequestId { get; set; }

        public string Token { get; set; }
        public string SqlConnectionString { get; set; }
        public object NoSqlConnectionString { get; set; }
    }
}
