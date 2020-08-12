using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer
{
   public interface IDomainEntity
    {
        int Id { get; set; }
        Boolean FillFromContext();
    }
}
