using System;
using System.Net.Http;

namespace RSSFeed.Services
{
    public class HttpClientManager
    {
        public static Lazy<HttpClient> HttpClient = new Lazy<HttpClient>();
    }
}
