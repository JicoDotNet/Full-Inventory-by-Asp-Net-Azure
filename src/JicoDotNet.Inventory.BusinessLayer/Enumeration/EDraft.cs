﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public enum EDraft
    {
        /// <summary>
        /// Purchase Order
        /// </summary>
        PO = 10,
        /// <summary>
        /// Purchase Order Amendment
        /// </summary>
        POA = 15,

        /// <summary>
        /// Goods Receive
        /// </summary>
        GRN = 20,
        /// <summary>
        /// Purchase Return
        /// </summary>
        PR = 25,

        /// <summary>
        /// Sales Order
        /// </summary>
        SO = 30,
        /// <summary>
        /// Sales Order Amendment
        /// </summary>
        SOA = 35,
        
        /// <summary>
        /// Delivery
        /// </summary>
        SHP = 40,
        /// <summary>
        /// Sales Return
        /// </summary>
        SR = 45,
    }
}
