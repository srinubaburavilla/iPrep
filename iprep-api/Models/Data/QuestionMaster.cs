using System;
using System.Collections.Generic;

namespace iprep_api.Models.Data
{
    public partial class QuestionMaster
    {
        public QuestionMaster()
        {
            IprepMappers = new HashSet<IprepMapper>();
        }

        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public int SubjectId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<IprepMapper> IprepMappers { get; set; }
    }
}
