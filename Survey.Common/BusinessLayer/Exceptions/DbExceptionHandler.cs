using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.Exceptions
{
    public class DbExceptionHandler
    {
        private Dictionary<int, DbError> ExceptionsMap { get; set; }
        static private DbExceptionHandler obj;
        private DbExceptionHandler()
        {
            this.fillExceptionsMap();
        }

        private void fillExceptionsMap()
        {
            this.ExceptionsMap = new Dictionary<int, DbError>();
            this.ExceptionsMap.Add(2601, new DbError(DbErrorType.Duplicate));
        }

        static public DbExceptionHandler GetInstance()
        {
            if (obj == null)
                obj = new DbExceptionHandler();
            return obj;
        }

        internal List<DbError> GetErrors(SqlException exception)
        {
            List<DbError> errs = new List<DbError>();
            foreach (SqlError item in exception.Errors)
            {
                if (this.ExceptionsMap.ContainsKey(item.Number))
                    errs.Add(this.ExceptionsMap[item.Number].Copy());
            }
            return errs;
        }

    }
}
