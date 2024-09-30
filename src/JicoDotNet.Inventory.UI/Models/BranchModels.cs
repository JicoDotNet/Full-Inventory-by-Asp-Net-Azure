using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class BranchModels
    {
        public Branch _branch { get; set; }
        public List<Branch> _branches { get; set; }
        public Dictionary<string, string> _State { get; set; }
    }
}