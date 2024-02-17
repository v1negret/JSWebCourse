using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models
{
    public class AddUnitResult
    {
        public ServiceResult Result { get; set; }
        public IEnumerable<HtmlParseError> Errors { get; set; }
    }
}
