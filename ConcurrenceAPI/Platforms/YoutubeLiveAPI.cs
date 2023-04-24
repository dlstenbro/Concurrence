using ConcurrenceAPI.Interfaces;
using ConcurrenceAPI.Models;
using ConcurrenceAPI.Models.Secrets;
using ConcurrenceAPI.Models.YouTube;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Policy;
using System.Text.Json;

namespace ConcurrenceAPI.Platforms
{
    public class YoutubeLiveAPI : IAPIEndpointConnector
    {
        #region YouTube Live Endpoints
        private const string _YoutubeAPISearchBase = @"https://youtube.googleapis.com/youtube/v3/search";
        private const string _YoutubeAPIVideosBase = @"https://youtube.googleapis.com/youtube/v3/videos";
        private const string _YoutubeAPIChannelBase = @"https://youtube.googleapis.com/youtube/v3/channel";
        private string _api_key;
        #endregion

        public YoutubeLiveAPI(string api_key) 
        {
            _api_key = api_key;
        }

        public RestRequest CreateRestRequest()
        {
            RestRequest req = new RestRequest();
            req.Method = Method.Get;
            req.AddHeader("cache-control", "no-cache");
            req.AddHeader("content-type", "application/x-www-form-urlencoded");

            return req;
        }

        public JsonDocument GetAPIResponse(string url, Dictionary<string, string> parameters = null)
        {
            RestRequest req = CreateRestRequest();
            RestClient client = new RestClient(url);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    req.AddOrUpdateParameter(parameter.Key, parameter.Value);
                }
            }

            req.AddOrUpdateParameter("key", _api_key);
            RestResponse res = client.Execute(req);

            return JsonDocument.Parse(res.Content);
        }

        public object GetStreams(Dictionary<string, string> parameters = null)
        {
            YoutubeLiveVideos streams = new YoutubeLiveVideos();
            streams.videos = new List<YoutubeLiveVideo>();

            JsonDocument res = GetAPIResponse(_YoutubeAPISearchBase, parameters);
            YoutubeLiveSearch search_results = JsonSerializer.Deserialize<YoutubeLiveSearch>(res);

            foreach(var result in search_results.items)
            {
                YoutubeLiveVideo video = GetVideo(result.id.videoId);
                streams.videos.Add(video);
            }
            streams.pageToken = search_results.nextPageToken;

            return streams;
        }
        private YoutubeLiveVideo GetVideo(string videoId, string part = "snippet,contentDetails,statistics,liveStreamingDetails")
        {
            var res = GetAPIResponse(_YoutubeAPIVideosBase, new Dictionary<string, string>()
                {
                    { "id", videoId },
                    { "part", part }
                });
            
            YoutubeLiveVideo video = JsonSerializer.Deserialize<YoutubeLiveVideo>(res);
            //var refChannel = GetChannel(video.items[0].snippet.channelId);
            //video.items[0].snippet.channelName = channel.items[0].id;
            return video;
        }
        public YoutubeLiveChannel GetChannel(string channelId, string part = "snippet")
        {
            //https://youtube.googleapis.com/youtube/v3/channels?part=snippet&id=UCCR6qrG1BV8edeqWE-xnH_Q
            var res = GetAPIResponse(_YoutubeAPIChannelBase, new Dictionary<string, string>()
                {
                    { "id", channelId },
                    { "part", part }
                });

            YoutubeLiveChannel channel = JsonSerializer.Deserialize<YoutubeLiveChannel>(res);
            return channel;
        }

        public object StreamSearchResults(string user_login = "", string game_name = "")
        {
            throw new System.NotImplementedException();
        }
    }
}
