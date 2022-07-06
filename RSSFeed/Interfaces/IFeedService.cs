using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RSSFeed.Models;

namespace RSSFeed.Interfaces
{
    public interface IFeedService
    {
        IList<FeedChannel> FeedChannels { get; }
        IList<FeedItem> MyFavoriteItems { get; }

        Task<bool> AddFeedChannelAsync(string url);
        Task<FeedChannel> GetFeedChannelAsync(string url);
        bool AddFeedItem(FeedItem feedItem);
        bool RemoveFeedItem(FeedItem feedItem);
    }
}
