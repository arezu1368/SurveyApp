using Survey.Common.BusinessLayer;
using Survey.Question.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Api.Models.Survey
{
    public class SurveyDTO : BaseDTO<Question.BusinessLayer.Survey>
    {
        public string Title { get; set; }
        public int CoordinatorId { get; set; }
        public bool IsActive { get; set; }
        public SurveyDTO(Question.BusinessLayer.Survey domainEntity = null)
            : base(domainEntity)
        {
        }

        public override bool FillFromDomainEntity(Question.BusinessLayer.Survey domainEntity)
        {
            if (!base.FillFromDomainEntity(domainEntity))
                return false;
            this.Title = domainEntity.Title;
            this.IsActive = domainEntity.IsActive;
            this.CoordinatorId = domainEntity.CoordinatorId;
            return true;
        }

        public override Question.BusinessLayer.Survey MakeFilledDomainEntity()
        {
            SurveyManager surveyManager = new SurveyManager();
            var survey = surveyManager.MakeSurveyInstance();
            survey.Id = this.Id;
            survey.Title = this.Title;
            survey.CoordinatorId = this.CoordinatorId;
            survey.IsActive = this.IsActive;
            return survey;
        }

    }
}