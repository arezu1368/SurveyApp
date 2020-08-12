using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Survey.Common.BusinessLayer.Validators
{
    public class NumberText : Attribute, IValidatorAttribute
    {
        public Boolean IsValid(object val)
        {
            if (val == null || val.ToString() == string.Empty)
                return true;
            Regex regex = new Regex("^[0-9]+$");
            Match match = regex.Match(val.ToString());
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}