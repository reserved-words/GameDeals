using GameDeals.Core.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDeals.Services
{
    public class HtmlParser : IHtmlParser
    {
        public string StripHtmlTags(string text)
        {
            var html = new HtmlDocument();
            html.LoadHtml(text);
            return html.DocumentNode.InnerText;
        }
    }
}
