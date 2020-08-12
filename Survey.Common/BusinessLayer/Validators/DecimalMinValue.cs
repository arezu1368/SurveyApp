using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Common.BusinessLayer.Validators
{
    public class DecimalMinValue : MinValue
    {
        public DecimalMinValue(object val)
            :base(val as Nullable<decimal>??Decimal.Zero)
        {
        }

    }
}