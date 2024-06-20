<<<<<<< HEAD
﻿using System.Collections.Generic;
=======
﻿using DataAccess.Sql.Entity;
using System.Collections.Generic;
>>>>>>> bb6919d0bdfc49e63abbcea8a0e4b0ea0f54f996

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
