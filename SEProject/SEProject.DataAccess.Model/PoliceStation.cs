using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SEProject.DataAccess.Model    
{
    public class PoliceStation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ClassifiedFile> ClassifiedFiles { get; set; }
    }
}
