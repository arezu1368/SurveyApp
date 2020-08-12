using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Common.BusinessLayer.Validators
{
    public class RequieredField : Attribute, IValidatorAttribute
    {
        public object EmptyVal { get; private set; }
        public RequieredField( object emptyVal)
        {
            this.EmptyVal = emptyVal;
        }
        public Boolean IsValid(object val)
        {
            if (val == null)
                return false;
            return ! val.Equals(this.EmptyVal);
        }
    }
}
