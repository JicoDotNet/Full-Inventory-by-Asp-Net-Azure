using System.Collections.Generic;

namespace DataAccess.Sql
{
    public class nameValuePairs : List<nameValuePair>{}
    public class nameValuePair
    {
        public nameValuePair(string iParmName, object iObjectValue)
        {
            getName = iParmName;
            getValue = iObjectValue;
        }

        public string getName { get; private set; }
        public object getValue { get; private set; }
    }
}
