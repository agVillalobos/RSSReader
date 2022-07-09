using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using RSSFeed.Models;

namespace RSSFeed.Parser
{
    public class FeedItemParser
    {
        public FeedChannel ParseFeedChannel(string response)
        {
            if (response == null) return null;

            XDocument doc = XDocument.Parse(response);
            XNamespace nsContent = doc.Root.GetNamespaceOfPrefix("content");
            XNamespace dcCreator = doc.Root.GetNamespaceOfPrefix("dc");

            List<FeedItem> feeds = new List<FeedItem>();
            var channel = doc.Root.Element("channel");

            DateTime.TryParse(channel?.Element("lastBuildDate")?.Value, out var lastBuildDate);

            var feedChannel = new FeedChannel()
            {
                Title = channel?.Element("title")?.Value,
                Link = channel?.Element("link")?.Value,
                Description = channel?.Element("description")?.Value,
                Language = channel?.Element("language")?.Value,
                Generator = channel?.Element("generator")?.Value,
                LastBuildDate = lastBuildDate,
                ImageUrl = channel?.Element("image")?.Element("url")?.Value
            };

            foreach (var item in doc.Descendants("item"))
            {
                DateTime.TryParse(item.Element("pubDate").Value.ToString(), out var dateTime);
                var imgs = GetImg(item.Element((nsContent ?? string.Empty) + "encoded")?.Value);
                var creator = item.Element((dcCreator ?? string.Empty) + "creator")?.Value;

                var feed = new FeedItem
                {
                    Title = item.Element("title").Value.ToString(),
                    Link = item.Element("link").Value.ToString(),
                    Description = item.Element("description").Value.ToString(),
                    PubDate = dateTime,
                    Guid = item.Element("guid").Value.ToString(),
                    Content = new FeedContent() { UrlImage = imgs },
                    Creator = creator
                };

                feeds.Add(feed);
            }

            feedChannel.Items = feeds;

            return feedChannel;
        }

        public string GetImg(string encoded)
        {
            if (encoded == null) return string.Empty;

            string pattern = "src=(?:\"|\')?(?<imgSrc>[^>]*[^/].(?:jpg|png))(?:\"|\')?";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var match = regex.Match(encoded);
            var domain = match.Groups["imgSrc"].Value;

            return domain;
        }
    }
}
