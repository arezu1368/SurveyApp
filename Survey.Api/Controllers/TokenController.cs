using Survey.Api.Models.User;
using Survey.Common.BusinessLayer.Jwt;
using Survey.Common.BusinessLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Survey.Api.Controllers
{
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        [AllowAnonymous]
        public string Post([FromBody] TokenRequestInfo reqInfo)
        {
            var user = Survey.Common.BusinessLayer.User.User.Login(reqInfo.UserName, reqInfo.Password);
            if (user != null)
            {
                return JwtManager.GenerateToken(user.Username);
            }
            return null;
        }
        [Route("getloggeduser")]
        [JwtAuthentication]
        public ResponseMessageResult GetLoggedUser()
        {
            var u = UserManager.GetCurrentUser();
            if (u != null)
            {
                UserDTO md = new UserDTO(u);
                return this.ResponseMessage(this.Request.CreateResponse<UserDTO>(md));
            }
            return this.ResponseMessage(this.Request.CreateResponse<bool>(HttpStatusCode.NotFound, false));

        }
    }
}
public class TokenRequestInfo
{
    public string UserName { get; set; }
    public string Password { get; set; }
}