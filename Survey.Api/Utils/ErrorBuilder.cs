
using Survey.Common.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Api.Utils
{
    public class ErrorBuilder
    {
        public List<ServerError> serverErrors { get; set; }
        public List<string> stringErrors { get; set; }
        public string InnerHtmlMsg { get; set; }


        public void Make()
        {
            this.FillFromStringError();
        }
        public static ErrorBuilder Make(String[] errors)
        {
            ErrorBuilder eb = new ErrorBuilder();
            eb.Clear();
            if (errors.Length > 0)
                eb.stringErrors = new List<string>(errors);
            eb.Make();
            return eb;
        }
        private void FillFromStringError()
        {
            string result = "";

            if (stringErrors != null && stringErrors.Count > 0)
            {
                result = "<ul>";
                foreach (string item in stringErrors)
                {
                    result += "<li>" + item + "</li>";
                }
                result += "</ul>";
                InnerHtmlMsg = "<i class='fa fa-warning'></i> خطا : " + result;
            }
        }


        public void Clear()
        {
            this.serverErrors = null;
            this.stringErrors = null;
        }
    }
}