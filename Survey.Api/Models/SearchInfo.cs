using Survey.Common.BusinessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Api.Models
{
    public class SearchInfo
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public int Skip
        {
            get
            {
                return (this.PageIndex - 1) * this.PageSize;
            }
        }
        public FetchMethod FetchMethod { get; set; } = FetchMethod.Newest;
        public string Title { get; set; }

    }
}