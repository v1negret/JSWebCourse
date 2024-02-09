using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models.Dto
{
    public class AddUnitDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlString {  get; set; }
        public int ChapterId { get; set; }
    }
}
