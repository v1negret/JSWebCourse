using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models
{
    public class Chapter
    {
        [Required]
        public int ChapterId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
