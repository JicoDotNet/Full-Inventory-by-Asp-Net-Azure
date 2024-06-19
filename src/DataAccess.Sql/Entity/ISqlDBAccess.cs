using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Sql.Entity
{
    public interface ISqlDBAccess
    {
        DataRow GetFirstOrDefaultRow(string Command, INameValuePairs NameValuePairObject);
        DataTable GetData(string Command, INameValuePairs NameValuePairObject);
        DataSet GetDataSet(string Command, INameValuePairs NameValuePairObject);
        object InsertUpdateDeleteReturnObject(string Command,
            INameValuePairs NameValuePairObject,
            string outParameterName);

    }
}
