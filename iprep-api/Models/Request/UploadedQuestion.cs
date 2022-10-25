using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iprep_api.Models.Request
{
    public class UploadedQuestion
    {
        [Required]
        public string Subject { get; set; } = null!;
        [Required]
        public string Question { get; set; } = null!;
        [Required]
        public string Answer { get; set; } = null!;
    }
}
