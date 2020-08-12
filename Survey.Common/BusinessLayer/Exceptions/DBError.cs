using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.Exceptions
{
    public class DbError : ServerError
    {
        public DbError(DbErrorType errorType)
        {

        }
        public object Obj { get; set; }

        //public PropertyInfo Property { get; set; }

        public DbErrorType ErrorType { get; private set; }

        public DbError Copy()
        {
            return new DbError(this.ErrorType)
            {
                Obj = this.Obj,
            };
        }
    }
    public enum DbErrorType
    {
        Duplicate = 2601,
    }
}
