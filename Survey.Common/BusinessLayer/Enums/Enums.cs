using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.Enums
{
    public enum States
    {
        UnChanged,
        Inserted,
        Updated,
        Deleted,
    }
    public enum FetchMethod : byte
    {
        None = 0,
        Newest = 1,
        Oldest = 2
    }
}
