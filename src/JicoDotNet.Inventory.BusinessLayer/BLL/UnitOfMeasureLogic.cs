using DataAccess.Sql;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class UnitOfMeasureLogic : ConnectionString
    {
        public UnitOfMeasureLogic(ICommonRequestDto CommonObj) : base(CommonObj) { }

        public string Set(UnitOfMeasure unitOfMeasure)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (unitOfMeasure.UnitOfMeasureId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {


                new NameValuePair("@UnitOfMeasureId", unitOfMeasure.UnitOfMeasureId),
                new NameValuePair("@UnitOfMeasureName", unitOfMeasure.UnitOfMeasureName),
                new NameValuePair("@Description", unitOfMeasure.Description),
                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetUnitOfMeasure]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string UnitOfMeasureId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "DEACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@UnitOfMeasureId", UnitOfMeasureId),


                new NameValuePair("@RequestId", CommonObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(GenericLogic.SqlSchema + ".[spSetUnitOfMeasure]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<UnitOfMeasure> Get()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData(GenericLogic.SqlSchema + ".[spGetUnitOfMeasure]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
                }).ToList<UnitOfMeasure>();
        }
    }
}
