using JicoDotNet.Inventory.Core.Entities;

namespace JicoDotNet.Inventory.Core.Common
{
    public sealed class CommonLogicHelper : ICommonLogicHelper
    {
        public string RequestId { get; set; }

        public string Token { get; set; }
        public string SqlSchema { get { return "[SingleIB]"; } }
        public string SqlConnectionString { get; set; }
        public object NoSqlConnectionString { get; set; }
    }
}
