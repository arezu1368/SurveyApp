using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Common.BusinessLayer.Validators
{
    public class ParamName : Attribute
    {
        public String Name { get; set; }
        public ParamName(String paramName)
        {
            this.Name = paramName;
        }
    }
}
