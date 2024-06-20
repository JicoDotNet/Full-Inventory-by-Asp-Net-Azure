using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

<<<<<<< HEAD
namespace DataAccess.Sql
=======
namespace DataAccess.Sql.Entity
>>>>>>> bb6919d0bdfc49e63abbcea8a0e4b0ea0f54f996
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
