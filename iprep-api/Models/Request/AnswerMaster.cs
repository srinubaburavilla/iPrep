using System;
using System.Collections.Generic;

namespace iprep_api.Models.Request
{
    public class AnswerMaster
    {
        public int Id { get; set; }
        public string Answer { get; set; } = null!;
        public int QuestionId { get; set; }
    }
}
