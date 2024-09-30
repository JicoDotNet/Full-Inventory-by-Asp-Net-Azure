using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class TDSModels
    {
        public List<TDSPay> _tDSPays { get; set; }
        public List<TDSReceive> _tDSReceives { get; set; }
        public Config _config { get; set; }
        public Dictionary<string, string> _state { get; set; }
    }
}