using Survey.Common.BusinessLayer.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey.Common.BusinessLayer.Exceptions
{
    public class RequierdFieldParser : ErrorParser
    {
        protected override ErrorParser Next
        {
            get
            {
                return new MinLenErrorParser(); ;
            }
            set
            {
                base.Next = value;
            }
        }

        protected override List<ServerError> filterErrors(List<ServerError> errors)
        {
            return errors.Where(e => e is ValidationError && ((ValidationError)e).Validator is RequieredField ).ToList();
        }

        protected override string makeErrorMessage(ServerError se)
        {
            ValidationError ve = (ValidationError)se;
            var propDesc = ve.Property.GetCustomAttributes(typeof(DisplayAttribute),true).FirstOrDefault();
            if (propDesc != null)
            {
                String propName = ((DisplayAttribute)propDesc).Name;
                return "فیلد '"+propName+"' الزامی است.";
            }
            return "";
        }
    }
}