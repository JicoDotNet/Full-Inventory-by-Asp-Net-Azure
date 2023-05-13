using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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