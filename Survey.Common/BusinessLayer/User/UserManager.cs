using Survey.Common.BusinessLayer.Exceptions;
using Survey.Common.BusinessLayer.Jwt;
using Survey.Common.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        static public User GetCurrentUser()
        {
            try
            {
                var httpContext = HttpContext.Current;
                if (httpContext.Request.Headers["Authorization"] == null)
                    return null;
                string tkn = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var p = JwtManager.GetPrincipal(tkn);
                UserManager um = new UserManager();
                var u = um.MakeUserInstance();
                u.Username = p.Identity.Name;
                if (!u.FillFromContext())
                    return null;
                return u;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
