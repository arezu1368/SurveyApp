using Survey.Api.Models;
using Survey.Api.Models.Survey;
using Survey.Api.Utils;
using Survey.Common.BusinessLayer.Exceptions;
using Survey.Common.BusinessLayer.Jwt;
using Survey.Question.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Survey.Api.Controllers
{
    [RoutePrefix("api/surveys")]
    public class SurveysController : ApiController
    {
        [HttpPost]
        [JwtAuthentication]
        public ResponseMessageResult Post([FromBody]SurveyDTO surveyDTO)
        {
            List<ServerError> errors = new List<ServerError>();
            var survey = surveyDTO.MakeFilledDomainEntity();
            if (survey.Save(ref errors))
            {
                return this.ResponseMessage(this.Request.CreateResponse(survey.Id));
            }
            ErrorParser ep = new ErrorParser();
            var errs = ep.MakeErrorMessages(errors);
            return this.ResponseMessage(this.Request.CreateResponse<string>(HttpStatusCode.InternalServerError, ErrorBuilder.Make(errs.ToArray()).InnerHtmlMsg));
        }
        [HttpGet]
        [AllowAnonymous]
        [JwtAuthentication]
        [Route("{id}")]
        public ResponseMessageResult Get(int id)
        {
            SurveyDTO dTO = new SurveyDTO()
            {
                Id = id,
            };
            SurveyManager sm = new SurveyManager();
            var survey = sm.MakeSurveyInstance();
            survey.Id = id;
            if (survey.FillFromContext())
            {
                dTO.FillFromDomainEntity(survey);
                return this.ResponseMessage(this.Request.CreateResponse(dTO));
            }
            return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.InternalServerError));
        }
        [HttpPost]
        [AllowAnonymous]
        [JwtAuthentication]
        [Route("list")]
        public ResponseMessageResult GetList([FromBody]SurveySearchInfo sInfo)
        {
            SurveyManager sm = new SurveyManager();
            int total;
            var list = sm.GetList(out total, sInfo.UserId, sInfo.IsActive, sInfo.Title, sInfo.FetchMethod,
                                            sInfo.PageSize, sInfo.Skip)
                                            .Select(s => new SurveyDTO(s)).ToList();
            CollectionResult<SurveyDTO> result = new CollectionResult<SurveyDTO>()
            {
                Total = total,
                ListDTO = list
            };
            return this.ResponseMessage(this.Request.CreateResponse(result));
        }
    }
    public class SurveySearchInfo : SearchInfo
    {
        public int UserId { get; }
        public bool? IsActive { get; }
    }
}