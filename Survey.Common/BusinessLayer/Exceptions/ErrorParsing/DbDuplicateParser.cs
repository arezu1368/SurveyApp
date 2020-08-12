using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.Exceptions
{
    public class DbDuplicateParser : ErrorParser
    {
        protected override ErrorParser Next
        {
            get
            {
                return null;
            }
            set
            {
                base.Next = value;
            }
        }

        protected override List<ServerError> filterErrors(List<ServerError> errors)
        {
            return errors.Where(a => a is DbError).ToList();
        }

        protected override string makeErrorMessage(ServerError a)
        {
            DbError aa = (DbError)a;
            var objDesc = aa.Obj.GetType().GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();
            if (objDesc != null)
            {
                String objName = ((DisplayAttribute)objDesc).Name;
                return "اطلاعات " + objName + " تکراری است.";
            }
            return "";
        }
    }
}
