using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer
{
    public abstract class BaseDTO<TDomainEntity> where TDomainEntity : IDomainEntity
    {
        public int Id { get; set; }
        public BaseDTO(TDomainEntity domainEntity)
        {
            this.FillFromDomainEntity(domainEntity);
        }

        public abstract TDomainEntity MakeFilledDomainEntity();

        public virtual bool FillFromDomainEntity(TDomainEntity domainEntity)
        {
            if (domainEntity == null)
                return false;
            this.Id = domainEntity.Id;
            return true;
        }
    }
}
