using System;
using System.Collections.Generic;

namespace iprep_api.Models.Data
{
    public partial class SubjectMaster
    {
        public SubjectMaster()
        {
            IprepMappers = new HashSet<IprepMapper>();
        }

        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<IprepMapper> IprepMappers { get; set; }
    }
}
