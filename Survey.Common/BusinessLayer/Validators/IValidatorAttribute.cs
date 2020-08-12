using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Common.BusinessLayer.Validators
{
    public interface IValidatorAttribute
    {
        Boolean IsValid(object val);
        
    }
}