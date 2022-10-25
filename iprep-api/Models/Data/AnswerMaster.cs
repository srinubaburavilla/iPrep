using System;
using System.Collections.Generic;

namespace iprep_api.Models.Data
{
    public partial class AnswerMaster
    {
        public AnswerMaster()
        {
            IprepMappers = new HashSet<IprepMapper>();
        }

        public int Id { get; set; }
        public string Answer { get; set; } = null!;
        public int QuestionId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<IprepMapper> IprepMappers { get; set; }
    }
}
