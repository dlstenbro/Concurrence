using ConcurrenceAPI.Common;
using ConcurrenceAPI.Models.Twitch;
using ConcurrenceAPI.Models.Secrets;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using ConcurrenceAPI.Interfaces;
using ConcurrenceAPI.Platforms;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace ConcurrenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformController : ControllerBase
    {
        protected readonly IConfiguration _config;

        private TwitchAPI _twitchAPI;

        #region Other
        #endregion

        #region Constructor
        public PlatformController(IConfiguration configuration)
        {
            _config = configuration;
            _twitchAPI = new TwitchAPI(_config["ClientId"], _config["ClientSecret"]);
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
    }
}
