using JSWebCourse.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Models
{
    public class GetChapterByIdResult
    {
        public ServiceResult ServiceResult { get; set; }
        public GetChapterDto Chapter { get; set; }
    }
}
