using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Sql.Entity
{
    public interface INameValuePair
    {
        string getName { get; }
        object getValue { get; }
    }
}
