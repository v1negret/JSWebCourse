using HtmlAgilityPack;
using JSWebCourse.Checks.Interfaces;
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
        public bool Validate(string htmlCode)
        {
            try
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlCode);

                var errors = htmlDoc.ParseErrors;

                if (errors.Any())
                {
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
