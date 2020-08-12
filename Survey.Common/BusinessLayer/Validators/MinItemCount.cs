using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.Common.BusinessLayer.Validators
{
    public class MinItemCount : Attribute, IValidatorAttribute
    {
        public int MinCount { get; private set; }
        public MinItemCount( int minCount)
        {
            this.MinCount = minCount;
        }
        public Boolean IsValid(object list)
        {
            ICollection lst = list as ICollection;
            return list == null || lst.Count >= this.MinCount;
        }
    }
}
