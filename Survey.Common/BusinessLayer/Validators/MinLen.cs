using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Common.BusinessLayer.Validators
{
    public class MinLen : Attribute, IValidatorAttribute
    {
        public int Len { get; private set; }
        public MinLen(int len)
        {
            this.Len = len;
        }

        public Boolean IsValid(object val)
        {
            String str = val as String ?? "";
            if( str.ToString().Length >= this.Len)
                return true;
            return false;
        }
    }
}
