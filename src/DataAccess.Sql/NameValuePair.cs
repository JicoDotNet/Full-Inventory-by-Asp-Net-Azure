using DataAccess.Sql.Entity;

namespace DataAccess.Sql
{
    public class NameValuePair : INameValuePair
    {
        public NameValuePair(string iParmName, object iObjectValue)
        {
            getName = iParmName;
            getValue = iObjectValue;
        }

        public string getName { get; private set; }
        public object getValue { get; private set; }
    }
}
