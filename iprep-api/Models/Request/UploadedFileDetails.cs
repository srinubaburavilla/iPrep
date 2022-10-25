using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iprep_api.Models.Request
{
    public class UploadedFileDetails
    {
        [Required]
        public string FileName { get; set; } = null!;
        [Required]
        public IFormFile File { get; set; } = null!;
    }
}
