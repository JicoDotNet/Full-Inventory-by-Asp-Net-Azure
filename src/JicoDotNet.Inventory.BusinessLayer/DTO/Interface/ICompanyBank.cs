﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.DTO.Interface
{
    public interface ICompanyBank
    {
        long CompanyBankId { get; set; }
        bool IsPrintable { get; set; }
    }
}
