using HtmlAgilityPack;
using JSWebCourse.Checks.Interfaces;
using JSWebCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JSWebCourse.Checks
{
    public class HtmlValidator : IHtmlValidator
    {
        public HtmlValidatorResult Validate(string htmlCode)
        {
            try
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlCode);

                var errors = htmlDoc.ParseErrors;

                if (errors.Any())
                {
                    return new HtmlValidatorResult() { Result = false, Errors = new List<HtmlParseError>(errors)};
                }

                return new HtmlValidatorResult() { Result = true };
            }
            catch (Exception ex)
            {
                return new HtmlValidatorResult() { Result = false };
            }
        }
    }
}
