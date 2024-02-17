using JSWebCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSWebCourse.Checks.Interfaces
{
    public interface IHtmlValidator
    {
        public HtmlValidatorResult Validate(string html);
    }
}
