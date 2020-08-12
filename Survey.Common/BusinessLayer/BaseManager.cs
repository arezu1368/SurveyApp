using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer
{
   public abstract class BaseManager<TContext> where TContext : DbContext
    {
        protected TContext Context { get; private set; }
        public BaseManager(TContext ctx)
        {
            this.Context = ctx;
        }

    }
}
