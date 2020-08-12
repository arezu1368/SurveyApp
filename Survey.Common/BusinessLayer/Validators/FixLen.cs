using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Common.BusinessLayer.Validators
{
    public class FixLen : Attribute, IValidatorAttribute
    {
        public int Len { get; private set; }
        public FixLen(int len)
        {
            this.Len = len;
        }

        public Boolean IsValid(object val)
        {
            if (val == null || val.ToString().Length == this.Len)
                return true;
            return false;
        }
    }
}
