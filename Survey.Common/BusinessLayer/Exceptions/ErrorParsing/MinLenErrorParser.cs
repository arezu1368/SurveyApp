
using Survey.Common.BusinessLayer.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey.Common.BusinessLayer.Exceptions
{
    public class MinLenErrorParser:ErrorParser
    {

        protected override ErrorParser Next
        {
            get
            {
                return new MinItemCountErrorParser();
            }
            set
            {
                base.Next = value;
            }
        }

        protected override List<ServerError> filterErrors(List<ServerError> errors)
        {
            return errors.Where(a => a is ValidationError && ((ValidationError)a).Validator is MinLen).ToList();
        }

        protected override string makeErrorMessage(ServerError se)
        {
            ValidationError ve = (ValidationError)se;
            var propDesc = ve.Property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
            if (propDesc != null)
            {
                String propName = ((DisplayNameAttribute)propDesc).DisplayName;
                String minlen = ((MinLen)ve.Validator).Len.ToString();
                return "فیلد '" + propName + "' می بایست دارای حداقل "+minlen+" کاراکتر باشد.";
            }
            return "";
        }
    }
}