using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using ConcurrenceAPI.Platforms;

using System.Collections.Generic;

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
        public object ShowDefault(int first, string after)
        {
            return _twitchAPI.GetStreams(first, after);
        }

        [HttpGet]
        [Route("/search")]
        public object Search(string user_login, string game_name)
        {
            return _twitchAPI.StreamSearchResults(user_login, game_name);
        }
        #endregion


        [HttpGet]
        [Route("/twitchStreams")]
        public object GetTwtichStreams(string first = "25", string after="")
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
