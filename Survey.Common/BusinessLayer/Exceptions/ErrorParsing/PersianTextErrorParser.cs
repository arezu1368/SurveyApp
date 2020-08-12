using Survey.Common.BusinessLayer.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.Exceptions
{
  public class PersianTextErrorParser:ErrorParser
    {
        protected override ErrorParser Next
        {
            get
            {
                return new DbDuplicateParser();
            }
            set
            {
                base.Next = value;
            }
        }
        protected override List<ServerError> filterErrors(List<ServerError> errors)
        {
            return errors.Where(a => a is ValidationError && ((ValidationError)a).Validator is PersianText).ToList();
        }
        protected override string makeErrorMessage(ServerError a)
        {
            ValidationError aa = (ValidationError)a;
            var propDesc = aa.Property.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();
            if (propDesc != null)
            {
                String propName = ((DisplayAttribute)propDesc).Name;
                return "لطفا فیلد "+propName+" را به فارسی تایپ کنید.";
            }
            return "";
        }
    }
}
