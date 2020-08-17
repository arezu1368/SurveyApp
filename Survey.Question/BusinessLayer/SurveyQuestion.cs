using Survey.Common.BusinessLayer;
using Survey.Common.BusinessLayer.Exceptions;
using Survey.Common.DataAccessLayer;
using Survey.Question.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Question.BusinessLayer
{
    public class SurveyQuestion : DomainEntity<SurveyQuestionEntities, TSurveyQuestion>
    {
        public SurveyQuestion(SurveyQuestionEntities context, IEntity entity = null)
            : base(context, entity ?? new DataAccessLayer.TSurvey())
        {
        }
        static internal SurveyQuestion Make(SurveyQuestionEntities context)
        {
            return new SurveyQuestion(context, null);
        }
        static internal SurveyQuestion Make(SurveyQuestionEntities context, IEntity entity)
        {
            if (entity == null)
                return null;
            var obj = new SurveyQuestion(context, entity);
            if (obj.Entity == null)
                return null;
            return obj;
        }

        [DisplayName("عنوان")]
        public String Title
        {
            get
            {
                return this.Entity.Title;
            }
            set
            {
                this.Entity.Title = value;
            }
        }
        public int QuestionTypeId
        {
            get
            {
                return this.Entity.QuestionTypeId;
            }
            set
            {
                this.Entity.QuestionTypeId = value;
            }
        }
        [DisplayName("زمان ثبت")]
        public DateTime RegisterMoment
        {
            get
            {
                return this.Entity.RegisterMoment;
            }
            set
            {
                this.Entity.RegisterMoment = value;
            }
        }

        [DisplayName("وضعیت فعال سازی")]
        public Boolean IsActive
        {
            get
            {
                return this.Entity.IsActive;
            }
            set
            {
                this.Entity.IsActive = value;
            }
        }
        protected override void Commit(ref List<ServerError> errors)
        {
            if (this.Id == 0)
            {
                this.RegisterMoment = DateTime.Now;
            }
            base.Commit(ref errors);
        }
    }
}
