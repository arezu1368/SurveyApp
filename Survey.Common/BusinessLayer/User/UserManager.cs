using Survey.Common.BusinessLayer.Exceptions;
using Survey.Common.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Common.BusinessLayer.User
{
  public  class UserManager: BaseManager<CommonEntities>
    {
        public UserManager()
         : base(new CommonEntities())
        {
        }
        public User MakeUserInstance()
        {
            return User.Make(this.Context);
        }
    }
}
