using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class JsonReturnModels
    {
        /// <summary>
        /// false = on Exception
        /// </summary>
        public bool _isSuccess { get; set; }
        /// <summary>
        /// Optional
        /// </summary>
        public int _status { get; set; }
        /// <summary>
        /// return model or Exception
        /// </summary>
        public object _returnObject { get; set; }
        public string _redirectURL { get; set; }
    }
}