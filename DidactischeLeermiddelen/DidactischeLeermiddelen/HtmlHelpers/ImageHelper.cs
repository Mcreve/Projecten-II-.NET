using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DidactischeLeermiddelen.HtmlHelpers
{
    public static class ImageHelper
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
    }
}