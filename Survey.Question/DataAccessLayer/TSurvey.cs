//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Survey.Question.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class TSurvey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TSurvey()
        {
            this.TSurveyQuestions = new HashSet<TSurveyQuestion>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RegisterMoment { get; set; }
        public System.DateTime LastChangeMoment { get; set; }
        public int CoordinatorId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TSurveyQuestion> TSurveyQuestions { get; set; }
        public virtual TUser TUser { get; set; }
    }
}
