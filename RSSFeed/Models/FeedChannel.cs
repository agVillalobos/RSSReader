using System;
using System.Collections;
using System.Collections.Generic;

namespace RSSFeed.Models
{
    public class FeedChannel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Generator { get; set; }
        public DateTime LastBuildDate { get; set; }
        public string ImageUrl { get; set; }
        public IList<FeedItem> Items { get; set; }
    }
}
