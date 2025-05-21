using System;
using System.ComponentModel.DataAnnotations;

namespace SEProject.DataAccess.Model
{
    public class ClassifiedFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
