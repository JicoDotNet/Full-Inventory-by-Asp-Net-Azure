﻿using JicoDotNet.Inventory.BusinessLayer.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Class
{
    public class QuotationDetail :  Product, IQuotationDetail, IActivity, IStatus,   IHRequest
    {
        public long QuotationDetailId { get; set; }
        public long QuotationId { get; set; }
        public string QuotationNumber { get; set; }

        public decimal Amount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }
}