using System;
using System.Collections.Generic;

namespace iprep_api.Models.Response
{
    public class SubjectMaster
    {
        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public DateTime Created { get; set; }
    }
}
