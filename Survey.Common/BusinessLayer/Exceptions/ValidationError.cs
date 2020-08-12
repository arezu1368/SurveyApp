
using Survey.Common.BusinessLayer.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace Survey.Common.BusinessLayer.Exceptions
{
    public class ValidationError :ServerError
    {
        public ValidationError()
            //:base(ErrorType.Validation)
        {

        }
        public object Obj { get; set; }
        public PropertyInfo Property { get; set; }
        public IValidatorAttribute Validator { get; set; } 

    }

}