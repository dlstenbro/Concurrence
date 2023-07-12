using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.Collections.Generic;
using System.Text.Json;

using ConcurrenceAPI.Models.Twitch;
using ConcurrenceAPI.Models.YouTube;
using ConcurrenceAPI.Models;
using ConcurrenceAPI.Platforms;
using System.Text;

namespace ConcurrenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformController : ControllerBase
    {
        protected readonly IConfiguration _config;

        #region Platforms
        private TwitchAPI _twitchAPI;
        private YoutubeLiveAPI _youtubeLiveAPI;
        #endregion

        #region Constructor
        public PlatformController(IConfiguration configuration)
        {
            _config = configuration;

            var secrets = GetSecrets();
            _twitchAPI = new TwitchAPI(secrets["twitch_client_id"], secrets["twitch_client_secret"]);
            _youtubeLiveAPI = new YoutubeLiveAPI(secrets["youtube_api_key"]);
        }
        #endregion Constructor

        #region Endpoints

        [HttpGet]
        [Route("/")]
        public object ShowDefault()
        {
            return populateModel();
        }

        [HttpGet]
        [Route("/search")]
        public object Search(string user_login, string game_name)
        {
            var targets = "";
            return targets;
        }
        #endregion

        [HttpGet]
        [Route("/twitchStreams")]
        public object GetTwitchStreams(string first = "25", string after="")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "first", first },
                { "after", after  }
            };


            return _twitchAPI.GetStreams(parameters);
        }


        [HttpGet]
        [Route("/youtubelive")]
        public object GetYoutubeLive(string part = "snippet", string eventType = "live", string maxResults = "25", string videoCategoryId = "20", string type = "video", string order ="viewCount", string q="gaming")
        {
            //https://youtube.googleapis.com/youtube/v3/search?part=snippet&eventType=live&maxResults=25&q=news&type=video

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "part", part },
                { "eventType", eventType },
                { "maxResults", maxResults },
                { "q", q },
                { "videoCategoryId", videoCategoryId},
                { "order", order},
                { "type", type }
            };

            return _youtubeLiveAPI.GetStreams(parameters);
        }

        #region Methods
        private ConcurrenceModel populateModel()
        {
            /*
             * Merge datasets into one unified dataset for concurrence to send back to clients.
             * 
             * May be a better way to do this but for now, Get JSON responses from all the platforms
             * then begin to convert them into the format we want to use for ConcurrenceModel.
            */
            ConcurrenceModel model = new ConcurrenceModel()
            {
                streams = new List<ConcurrenceModel.Data>()
            };

            TwitchAPIModel twitch_streams = JsonSerializer.Deserialize<TwitchAPIModel>((JsonDocument)GetTwitchStreams());
            YoutubeLiveVideos youtubeLive_streams = (YoutubeLiveVideos)GetYoutubeLive();

            /*
             * For right now, store paging information for each platform underneath another section
             * in the response. When "next" or "previous" is used on the angular side, 
             * we can just reference those tokens easier.
             */
            model.youtubelive_pageination = youtubeLive_streams.pageToken;
            model.twitch_pageination = twitch_streams?.pagination.cursor;

            foreach (var s in twitch_streams.data)
            {
                model.streams.Add(new ConcurrenceModel.Data()
                {
                    creator_name = s.user_name,
                    dateTime = s.started_at,
                    game_name = s.game_name,
                    id = s.id,
                    language = s.language,
                    platform = s.platform,
                    tags = s.tag_ids,
                    thumbnail_img = s.thumbnail_url,
                    title = s.title,
                    viewers = s.viewer_count
                });
            }

            foreach (YoutubeLiveVideo s in youtubeLive_streams.videos)
            {
                var item = s.items[0];

                model.streams.Add(new ConcurrenceModel.Data()
                {
                    creator_name = item.snippet.channelTitle,
                    dateTime = item.liveStreamingDetails.actualStartTime,
                    game_name = item?.snippet?.refChannelName,
                    id = item.id,
                    language = item.snippet.defaultLanguage,
                    platform = item.platform,
                    tags = item.snippet.tags,
                    thumbnail_img = item.snippet.thumbnails.high.url,
                    title = item.snippet.title,
                    viewers = string.IsNullOrEmpty(item.liveStreamingDetails.concurrentViewers) ? 0 : int.Parse(item.liveStreamingDetails.concurrentViewers)
                });
            }

            return model;
        }

        private Dictionary<string, string> GetSecrets()
        {
            var secrets = @"/run/secrets";

            string twitch_client_id = System.IO.File.ReadAllText($"{secrets}/twitch_client_id", Encoding.UTF8);
            string twitch_client_secret = System.IO.File.ReadAllText($"{secrets}/twitch_client_secret", Encoding.UTF8);
            string youtube_api_key = System.IO.File.ReadAllText($"{secrets}/youtube_api_key", Encoding.UTF8);

            return new Dictionary<string, string> { 
                { "twitch_client_id", twitch_client_id },
                { "twitch_client_secret", twitch_client_secret },
                { "youtube_api_key", youtube_api_key }
            };
        }
        #endregion Methods


    }
}
