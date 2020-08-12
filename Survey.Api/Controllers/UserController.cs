using Survey.Api.Models.User;
using Survey.Api.Utils;
using Survey.Common.BusinessLayer.Exceptions;
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
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        [Route("register")]
        public ResponseMessageResult Register([FromBody]UserDTO userInfo)
        {
            List<ServerError> errors = new List<ServerError>();
            UserManager userManager = new UserManager();
            UserDTO userDTO = userInfo;
            var user = userInfo.MakeFilledDomainEntity();
            var result = user.Save(userDTO.Password,ref errors);
            if (result)
            {
                return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.OK));
            }
            ErrorParser ep = new ErrorParser();
            var errs = ep.MakeErrorMessages(errors);
            return this.ResponseMessage(this.Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ErrorBuilder.Make(errs.ToArray()).InnerHtmlMsg));
        }
    }
    public class UserRegisterInfo
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
    }
}
