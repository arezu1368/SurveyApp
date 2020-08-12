
using Survey.Common.BusinessLayer.Validators;
using System;
using System.Collections.Generic;
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

        protected override string makeErrorMessage(ServerError a)
        {
            ValidationError aa = (ValidationError)a;
            var propDesc = aa.Property.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();
            if (propDesc != null)
            {
                String propName = ((DisplayAttribute)propDesc).Name;
                String minlen = ((MinLen)aa.Validator).Len.ToString();
                return "فیلد '" + propName + "' می بایست دارای حداقل "+minlen+" کاراکتر باشد.";
            }
            return "";
        }
    }
}