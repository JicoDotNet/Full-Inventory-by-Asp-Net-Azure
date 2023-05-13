using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class UnitOfMeasureLogic : ConnectionString
    {
        public UnitOfMeasureLogic(sCommonDto CommonObj) : base(CommonObj) { }

        public string Set(UnitOfMeasure unitOfMeasure)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (unitOfMeasure.UnitOfMeasureId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@UnitOfMeasureId", unitOfMeasure.UnitOfMeasureId),
                new nameValuePair("@UnitOfMeasureName", unitOfMeasure.UnitOfMeasureName),
                new nameValuePair("@Description", unitOfMeasure.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetUnitOfMeasure]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string UnitOfMeasureId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "DEACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@UnitOfMeasureId", UnitOfMeasureId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetUnitOfMeasure]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<UnitOfMeasure> Get()
        {
            return new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetUnitOfMeasure]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<UnitOfMeasure>();
        }
    }
}
