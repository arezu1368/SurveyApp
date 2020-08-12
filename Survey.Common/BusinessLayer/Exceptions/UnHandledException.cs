
using Survey.Common.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace Bsn.Common.BusinessLayer.Exceptions
{
    public class UnHandledException :ServerError
    {
        public UnHandledException()
            //:base(ErrorType.Validation)
        {

        }
        public object Obj { get; set; }
        public Exception ThrownException { get; set; }

    }

}