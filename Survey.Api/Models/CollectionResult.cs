using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Api.Models
{
    public class CollectionResult<T>
    {
        public int Total { get; set; }

        public List<T> ListDTO { get; set; }
    }
}