using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using GameDeals.Core.Interfaces;

namespace GameDeals.Services
{
    public class SyndicationFeedService : ISyndicationFeedService
    {
        public SyndicationItem[] GetPosts(string feedUrl)
        {
            using (var xmlReader = XmlReader.Create(feedUrl, new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                MaxCharactersFromEntities = 1024
            }))
            {
                var syndicationFeed = SyndicationFeed.Load(xmlReader);

                return syndicationFeed.Items.ToArray();
            }
        }
    }
}
