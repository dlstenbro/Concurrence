using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;

namespace ConcurrenceAPI.Models.YouTube
{
    public class YoutubeAPIBase
    {
        public string platform { get; set; } = "Youtube";
        public string kind { get; set; }
        public string etag { get; set; }
        public string regionCode { get; set; }
        public PageInfo pageInfo { get; set; }
        public class PageInfo
        {
            public int totalResults { get; set; }
            public int resultsPerPage { get; set; }
        }
    }
    public class YoutubeLiveSearch : YoutubeAPIBase
    {
                public string nextPageToken { get; set; }
        public  List<Item> items { get; set; }

        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public  ID id {  get; set; }
            public class ID
            {
                public string kind { get; set; }
                public string videoId { get; set; }
            }
            public Snippet snippet { get; set; }
            public class Snippet
            {
                public DateTime publishedAt { get; set; }
                public string channelId { get; set; }
                public string title { get; set; }
                public string description { get; set; }

                public Thumbnails thumbnails { get; set; }

                public class Thumbnails
                {
                    public Default @default { get; set; }
                    public Medium medium { get; set; }
                    public High high { get; set; }
                    public  class Default
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public  class Medium
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public  class High
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                }
                public string channelTitle { get; set; }
                public string liveBroadcastContent { get; set; }
                public DateTime publishTime { get; set; }
            }
        }
    }

    public class YoutubeLiveVideo : YoutubeAPIBase
    {
        public List<Item> items { get; set; }

        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
            public string platform { get; set; } = "Youtube";
            public class Snippet
            {
                public DateTime publishedAt { get; set; }
                public string channelId { get; set; }
                public string refChannelName { get; set; }
                public string title { get; set; }
                public string description { get; set; }

                public Thumbnails thumbnails { get; set; }

                public class Thumbnails
                {
                    public Default @default { get; set; }
                    public Medium medium { get; set; }
                    public High high { get; set; }
                    public class Default
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public class Medium
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public class High
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                }
                public string channelTitle { get; set; }
                public List<string> tags { get; set; }
                public string categoryId { get; set; }
                public string defaultLanguage { get; set; }
                public string language { get; set; }
                public string defaultAudioLanguage { get; set; }
                public Localized localized { get; set; }
                public class Localized
                {
                    public string title { get; set; }
                    public string description { get; set; }
                }
                public string liveBroadcastContent { get; set; }
                public DateTime publishTime { get; set; }
            }
            public ContentDetails contentDetails { get; set; }
            public class ContentDetails
            {
                public string duration { get; set; }
                public string dimension { get; set; }
                public string definition { get; set; }
                public string caption { get; set; }
                public bool licensedContent { get; set; }
                public ContentRating contentRating { get; set; }
                public class ContentRating
                {

                }
                public string projection { get; set; }
            }
            public Statistics statistics { get; set; }
            public class Statistics
            {
                public string viewCount { get; set; }
                public string likeCount { get; set; }
                public string favoriteCount { get; set; }
                public string commentCount { get; set; }
            }
            public LiveStreamingDetails liveStreamingDetails { get; set; }
            public class LiveStreamingDetails
            {
                public DateTime actualStartTime { get; set; }
                public string concurrentViewers { get; set; }

            }
        }
    }

    public class YoutubeLiveVideos
    {
        public List<YoutubeLiveVideo> videos { get; set; }
        public string pageToken { get; set; }
    }

    public class YoutubeLiveChannel : YoutubeAPIBase
    {
        public List<Item> items { get; set; }
        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public Snippet snippet { get; set; }
            public class Snippet
            {
                public DateTime publishedAt { get; set; }
                public string title { get; set; }
                public string description { get; set; }

                public Thumbnails thumbnails { get; set; }

                public class Thumbnails
                {
                    public Default @default { get; set; }
                    public Medium medium { get; set; }
                    public High high { get; set; }
                    public class Default
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public class Medium
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public class High
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                }
                public Localized localized { get; set; }
                public class Localized
                {
                    public string title { get; set; }
                    public string description { get; set; }
                }
            }
        }
    }
}