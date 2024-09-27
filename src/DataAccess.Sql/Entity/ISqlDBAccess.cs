using System.Collections.Generic;
using System.Data;

namespace DataAccess.Sql.Entity
{
    public interface ISqlDBAccess
    {
        DataRow GetFirstOrDefaultData(string command, INameValuePairs nameValuePairObject);
        DataTable GetData(string command, INameValuePairs nameValuePairObject);
        DataSet GetDataSet(string command, INameValuePairs nameValuePairObject);
        object DataManipulation(string spName,
            INameValuePairs nameValuePairObject,
            string outParameterName);

        INameValuePairs DataManipulation(string spName,
            INameValuePairs inParameterName,
            IList<string> outParameterName = null);
    }
}
