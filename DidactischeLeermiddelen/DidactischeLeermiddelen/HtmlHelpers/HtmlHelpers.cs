using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace DidactischeLeermiddelen.HtmlHelpers
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Convert to <img></img> in html with Razor.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src"></param>
        /// <param name="altText"></param>
        /// <param name="cssClass"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string cssClass = null, string height = null, string width = null)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);

            if (!string.IsNullOrWhiteSpace(height))
            {
                builder.MergeAttribute("height", height);
            }
            if (!string.IsNullOrWhiteSpace(width))
            {
                builder.MergeAttribute("width", width);
            }
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                builder.MergeAttribute("class", cssClass);
            }
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    

        /// <summary>
        /// Convert to <a></a> in html with Razor.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static MvcHtmlString MailTo(this HtmlHelper helper, string email)
        {
            return string.IsNullOrEmpty(email) ? null : MvcHtmlString.Create(string.Format("<a href='mailto:{0}'>{0}</a>", email));
        }

        public static MvcHtmlString Url(this HtmlHelper helper, string url)
        {
            return string.IsNullOrEmpty(url) ? null : MvcHtmlString.Create(string.Format("<a href='http://{0}' target='_blank' >{0}</a>", url));

        }
    }
}

