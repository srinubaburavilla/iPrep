using System;
using System.Collections.Generic;

namespace iprep_api.Models.Request
{
    public class IprepMapper
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
