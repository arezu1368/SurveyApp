
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Survey.Common.BusinessLayer.Exceptions
{
    public class ErrorParser
    {

        protected virtual ErrorParser Next
        {
            get
            {
                return new RequierdFieldParser();
            }
            set
            {
                this.Next = value;
            }
        }

        //public string ToString(List<ServerError> errors)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    this.MakeErrorMessages(errors).ForEach(a=>sb.AppendLine(a));
        //    return sb.ToString();
        //}

        public List<string> MakeErrorMessages(List<ServerError> errors)
        {
            List<String> errs = new List<String>();
            List < ServerError > ferrs = this.filterErrors(errors);
            ferrs.ForEach(e=>errs.Add(this.makeErrorMessage(e)));
            errors.RemoveAll(e=>ferrs.Contains(e));
            if(this.Next != null && errors.Any())
                errs.AddRange(this.Next.MakeErrorMessages(errors));
            return errs;
        }

        protected virtual string makeErrorMessage(ServerError se)
        {
            return "";
        }

        protected virtual List<ServerError> filterErrors(List<ServerError> errors)
        {
            return new List<ServerError>();
        }
    }
}