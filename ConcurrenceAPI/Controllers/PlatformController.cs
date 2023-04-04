using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using ConcurrenceAPI.Platforms;

using System.Collections.Generic;
using System.Text.Json;
using ConcurrenceAPI.Models.Twitch;
using ConcurrenceAPI.Models.YouTube;
using ConcurrenceAPI.Models;
using System;
using System.ComponentModel.Design;

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
            _twitchAPI = new TwitchAPI(_config["ClientId"], _config["ClientSecret"]);
            _youtubeLiveAPI = new YoutubeLiveAPI(_config["YTAPIKey"]);
        }
        #endregion Constructor

        #region Endpoints

        [HttpGet]
        [Route("/")]
        public object ShowDefault()
        {
            /*
             * Merge datasets into one unified dataset for concurrence to send back to clients.
             * 
             * May be a better way to do this but for now, Get JSON responses from all the platforms
             * then begin to convert them into the format we want to use for ConcurrenceModel.
            */
            ConcurrenceModel model = new ConcurrenceModel();
            model.streams = new List<ConcurrenceModel.Data>();

            TwitchAPIModel twitch_streams = (TwitchAPIModel)GetTwitchStreams();
            YoutubeLiveModel youtubeLive_streams = (YoutubeLiveModel)GetYoutubeLive();
            
            /*
             * For right now, store paging information for each platform underneath another section
             * in the response. When "next" or "previous" is used on the angular side, 
             * we can just reference those tokens easier.
             */
            model.youtubelive_pageination = youtubeLive_streams.nextPageToken;
            model.twitch_pageination = twitch_streams.pagination.cursor;

            foreach (var s in twitch_streams.data)
            {
                model.streams.Add(new ConcurrenceModel.Data()
                {
                    id = s.id,
                    game_name = s.game_name,
                    creator_name = s.user_name,
                    title = s.title,
                    thumbnail_img = s.thumbnail_url,
                    tags = s.tag_ids,
                    platform = s.platform,
                    language = s.language,
                    viewers = s.viewer_count,
                    dateTime = s.started_at
                });
            }

            foreach(var s in youtubeLive_streams.items)
            {
                model.streams.Add(new ConcurrenceModel.Data()
                {
                    id = s.id.videoId,
                    creator_name = s.snippet.channelTitle,
                    title = s.snippet.title,
                    dateTime = s.snippet.publishTime,
                    thumbnail_img = s.snippet.thumbnails.high.url,
                    platform = s.platform,
                }) ;
            }

            return model;
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
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "first", first },
                { "after", after  }
            };


            return _twitchAPI.GetAPIResponse(parameters);
        }


        [HttpGet]
        [Route("/youtubelive")]
        public object GetYoutubeLive(string part = "snippet", string eventType = "live", string maxResults = "25", string q = "games", string type = "video")
        {
            //https://youtube.googleapis.com/youtube/v3/search?part=snippet&eventType=live&maxResults=25&q=news&type=video

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "part", part },
                { "eventType", eventType },
                { "maxResults", maxResults },
                { "q", q },
                { "type", type }
            };

            return _youtubeLiveAPI.GetAPIResponse(parameters);
        }
    }
}
