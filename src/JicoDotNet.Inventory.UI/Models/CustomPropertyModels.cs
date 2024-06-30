using Microsoft.WindowsAzure.Storage.Table;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class CustomPropertyModels
    {
        public Dictionary<ECustomPropertyFor,int> _propertiesCount { get; set; }
        public ECustomPropertyFor _customPropertyFor { get; set; }
        public List<CustomProperty> _customProperties { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
        public IDictionary<EdmType, string> _dataType { get; set; }
        public CustomProperty _customProperty  { get; set; }
        public string _rowKey { get; set; }
    }
}