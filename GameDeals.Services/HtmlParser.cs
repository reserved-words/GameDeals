using GameDeals.Core.Interfaces;
using HtmlAgilityPack;

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
