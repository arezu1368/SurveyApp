using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Common.BusinessLayer.Validators
{
    public class MinValue : Attribute, IValidatorAttribute
    {
        public IComparable MinVal { get; private set; }
        public MinValue(object val)
        {
            this.MinVal = (IComparable)val;
        }

        public Boolean IsValid(IComparable val)
        {
            if (val is Decimal)
            {
                Decimal d = Convert.ToDecimal(this.MinVal);
                if (val.CompareTo((Decimal)d) < 0)
                    return false;
            }

            if ( val.CompareTo(this.MinVal) >= 0)
                return true;
         
            return false;
        }

        public bool IsValid(object val)
        {
            return this.IsValid(val as IComparable);
        }
    }
}
