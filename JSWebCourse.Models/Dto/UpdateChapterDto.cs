using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models.Dto
{
    public class UpdateChapterDto
    {
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
