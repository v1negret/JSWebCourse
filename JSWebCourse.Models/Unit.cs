using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models
{
    public class Unit
    {
        [Required]
        public int UnitId {  get; set; }
        [Required]
        [MaxLength(30)]
        public string Title {  get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(15000)]
        public string HtmlCode {  get; set; }
        [Required]
        public int ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
    }
}
