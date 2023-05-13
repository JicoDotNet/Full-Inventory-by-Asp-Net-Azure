using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class BranchModels
    {
        public Branch _branch { get; set; }
        public List<Branch> _branches { get; set; }
        public Dictionary<string, string> _State { get; set; }
    }
}