using System;
using System.Collections.Generic;

namespace iprep_api.Models.Response
{
    public class QuestionMaster
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public int SubjectId { get; set; }
    }
}
