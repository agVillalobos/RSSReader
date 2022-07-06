using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RSSFeed.Interfaces;
using RSSFeed.Models;
using RSSFeed.Parser;
using System.Linq;

namespace RSSFeed.Services
{
    public class FeedService : IFeedService
    {
        private readonly HttpClient httpClient;
        private IList<FeedChannel> feedChannels;
        private IList<string> defaultFeedChannels = new List<string>()
        {
            //"https://www.nytimes.com/svc/collections/v1/publish/https://www.nytimes.com/section/world/rss.xml",
            "https://feeds.npr.org/93559255/rss.xml",
            //"http://feeds.bbci.co.uk/news/world/rss.xml",
            //"http://feeds.foxnews.com/foxnews/latest",
            "https://feeds.npr.org/1001/rss.xml",
            //"http://feeds.sciencedaily.com/sciencedaily",
            //"https://rss.nytimes.com/services/xml/rss/nyt/Sports.xml"
            //"http://feeds.feedburner.com/TheDailyPuppy"
        };
        private IList<FeedItem> myFavoriteItems;

        public FeedService()
        {
            this.httpClient = HttpClientManager.HttpClient.Value;
            this.feedChannels = new List<FeedChannel>();
            myFavoriteItems = new List<FeedItem>();

            _ = InitializeFeedChannels();
        }

        private async Task InitializeFeedChannels()
        {
            foreach (var url in defaultFeedChannels)
            {
                await this.AddFeedChannelAsync(url);
            }
        }

        public IList<FeedChannel> FeedChannels => this.feedChannels;

        public IList<FeedItem> MyFavoriteItems => this.myFavoriteItems;

        public async Task<bool> AddFeedChannelAsync(string url)
        {
            try
            {
                var feedChannel = await GetFeedChannelAsync(url);
                if (feedChannel == null) return false;

                this.feedChannels.Add(feedChannel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public async Task<FeedChannel> GetFeedChannelAsync(string url)
        {
            FeedChannel feedChannel = null;
            try
            {
                var uri = new Uri(url);
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();
                var parser = new FeedItemParser();
                feedChannel = parser.ParseFeedChannel(responseString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return feedChannel;
        }

        public bool AddFeedItem(FeedItem feedItem)
        {
            var myFavoriteItem = myFavoriteItems.FirstOrDefault(l => l.Guid == feedItem.Guid);

            if (myFavoriteItem != null) return false;

            myFavoriteItems.Add(feedItem);

            return true;
        }

        public bool RemoveFeedItem(FeedItem feedItem)
        {
            var myFavoriteItem = myFavoriteItems.FirstOrDefault(l => l.Guid == feedItem.Guid);

            if (myFavoriteItem == null) return false;

            myFavoriteItems.Remove(feedItem);

            return true;
        }
    }
}
