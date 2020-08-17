using Survey.Api.Models.User;
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
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        [Route("register")]
        public ResponseMessageResult Register([FromBody]UserDTO userInfo)
        {
            List<ServerError> errors = new List<ServerError>();
            UserManager userManager = new UserManager();
            UserDTO userDTO = userInfo;
            var user = userInfo.MakeFilledDomainEntity();
            var result = user.Save(userDTO.Password, ref errors);
            if (result)
            {
                return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.OK));
            }
            ErrorParser ep = new ErrorParser();
            var errs = ep.MakeErrorMessages(errors);
            return this.ResponseMessage(this.Request.CreateResponse<string>(HttpStatusCode.InternalServerError, Utils.ErrorBuilder.Make(errs.ToArray()).InnerHtmlMsg));
        }
    }
}
