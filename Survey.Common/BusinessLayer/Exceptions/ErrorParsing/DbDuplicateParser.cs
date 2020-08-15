using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        protected override string makeErrorMessage(ServerError se)
        {
            DbError de = (DbError)se;
            var objDesc = de.Obj.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
            if (objDesc != null)
            {
                String objName = ((DisplayNameAttribute)objDesc).DisplayName;
                return "اطلاعات " + objName + " تکراری است.";
            }
            return "";
        }
    }
}
