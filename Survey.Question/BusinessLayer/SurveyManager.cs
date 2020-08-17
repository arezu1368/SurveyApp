using Survey.Common.BusinessLayer.Enums;
using Survey.Question.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Question.BusinessLayer
{
    public class SurveyManager : Common.BusinessLayer.BaseManager<SurveyQuestionEntities>
    {
        public SurveyManager()
         : base(new SurveyQuestionEntities())
        {
        }
        public Survey MakeSurveyInstance()
        {
            return Survey.Make(this.Context);
        }
        public List<Survey> GetList(out int total,int userId,bool? isActive,
        string title,FetchMethod fetchMethod, int top, int skip)
        {
            IQueryable<TSurvey> tDocuments = this.Context
                .TSurveys
                .Include("TUser")
                .AsQueryable();
            if (userId > 0)
                tDocuments = tDocuments.Where(d => d.CoordinatorId == userId);
            if (isActive.HasValue)
                tDocuments = tDocuments.Where(d => d.IsActive == isActive);

            if (!String.IsNullOrEmpty(title) && !String.IsNullOrWhiteSpace(title))
                tDocuments = tDocuments.Where(d => d.Title.Contains(title));
        
            switch (fetchMethod)
            {
                case FetchMethod.Newest:
                    tDocuments = tDocuments.OrderByDescending(d => d.RegisterMoment);
                    break;
                default:
                    tDocuments = tDocuments.OrderByDescending(d => d.RegisterMoment);
                    break;
            }
            total = tDocuments.Count();

            var list = tDocuments
                .Skip(skip)
                .Take(top)
                .ToList()
                .Select(a => Survey.Make(this.Context, a))
                .ToList();
            return list;
        }
    }
}
