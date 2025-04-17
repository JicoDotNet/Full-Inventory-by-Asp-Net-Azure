﻿using DataAccess.AzureStorage.Table;
using JicoDotNet.Inventory.Core.Enumeration;
using JicoDotNet.Inventory.Core.Models;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class CustomPropertyModels
    {
        public Dictionary<ECustomPropertyFor, int> _propertiesCount { get; set; }
        public ECustomPropertyFor _customPropertyFor { get; set; }
        public List<CustomProperty> _customProperties { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
        public IDictionary<EdmType, string> _dataType { get; set; }
        public CustomProperty _customProperty { get; set; }
        public string _rowKey { get; set; }
    }
}