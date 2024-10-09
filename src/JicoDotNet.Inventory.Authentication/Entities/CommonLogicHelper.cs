using JicoDotNet.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Authentication.Entities
{
    public sealed class CommonLogicHelper : ICommonLogicHelper
    {
        public string RequestId { get; set; }

        public string Token { get; set; }
        public string SqlSchema { get; private set; }
        public string SqlConnectionString { get; set; }
        public object NoSqlConnectionString { get; set; }

        internal void SetSchema(string schemaName)
        {
            SqlSchema = schemaName;
        }
    }
}
