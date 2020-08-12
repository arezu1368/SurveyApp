
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Survey.Common.BusinessLayer.Validators
{
    public class PersianText : Attribute, IValidatorAttribute
    {
        public Boolean IsValid(object val)
        {
            if (val == null)
                return true;
            Regex regex = new Regex(@"^[\u0600-\u06FF ]+$");
            Match match = regex.Match(val.ToString());
            if (match.Success)
                return true;
            else
                return false; 
        }
    }
}
