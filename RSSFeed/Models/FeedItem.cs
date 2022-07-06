using System;

namespace RSSFeed.Models
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public string Guid { get; set; }
        public FeedContent Content { get; set; }
        public string Creator { get; set; }
        public bool Favorite { get; set; }
    }
}
