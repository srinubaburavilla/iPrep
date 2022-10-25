using System;
using System.Collections.Generic;

namespace iprep_api.Models.Data
{
    public partial class IprepMapper
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AnswerMaster Answer { get; set; } = null!;
        public virtual QuestionMaster Question { get; set; } = null!;
        public virtual SubjectMaster Subject { get; set; } = null!;
    }
}
