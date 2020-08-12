using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace Survey.Common.BusinessLayer.Exceptions
{
    public class ServerError
    {
        public ServerError()
        {
        }
        //private ErrorType Type { get; set; }
    }

    public enum ErrorType
    {
        UnKnown,
        ServerState,
        Validation,
        BusinessRule,
    }
}