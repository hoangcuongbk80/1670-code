using System.ComponentModel.DataAnnotations;

namespace FPTJobMatch.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Company { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime PostedDate { get; set; }
    }
}
