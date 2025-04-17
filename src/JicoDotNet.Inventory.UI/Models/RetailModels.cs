﻿using JicoDotNet.Inventory.Core.Entities;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;

namespace JicoDotNet.Inventory.UI.Models
{
    public class RetailModels
    {
        public ICompanyBasic _company { get; set; }
        public string[] _customersNumber { get; set; }
        public Dictionary<string, string> _state { get; set; }
        public Dictionary<string, string> _companyType { get; set; }
        public IDictionary<bool, string> _YesNo { get; set; }
        public Config _config { get; set; }
        public List<WareHouse> _wareHouses { get; set; }

        public DateTime _dateLimit { get; set; }
    }
}