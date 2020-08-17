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
   public class Survey: DomainEntity<SurveyQuestionEntities, TSurvey>
    {
        public Survey(SurveyQuestionEntities context, IEntity entity = null)
            : base(context, entity ?? new DataAccessLayer.TSurvey())
        {
        }
        static internal Survey Make(SurveyQuestionEntities context)
        {
            return new Survey(context,null);
        }
        static internal Survey Make(SurveyQuestionEntities context, IEntity entity)
        {
            if (entity == null)
                return null;
            var obj = new Survey(context, entity);
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
        [DisplayName("نام و نام خانوادگی")]
        public int CoordinatorId
        {
            get
            {
                return this.Entity.CoordinatorId;
            }
            set
            {
                this.Entity.CoordinatorId = value;
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
        [DisplayName("زمان آخرین تغییر")]
        public DateTime LastChangeMoment
        {
            get
            {
                return this.Entity.LastChangeMoment;
            }
            set
            {
                this.Entity.LastChangeMoment = value;
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
            this.LastChangeMoment = DateTime.Now;
            base.Commit(ref errors);
        }
    }
}
