using ConcurrenceAPI.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace ConcurrenceAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        protected readonly IConfiguration _config;

        #region Twitch Endpoints
        protected string _TwitchStreamsURL = @"https://api.twitch.tv/helix/streams";
        protected string _TwitchGamesURL = @"https://api.twitch.tv/helix/games";
        protected string _TwitchAuthURL = @"https://id.twitch.tv/oauth2/token";
        #endregion

        #region YouTube Live Endpoints
        protected string _YoutubeLiveStreamsURL = @"";
        protected string _YoutubeLiveGamesURL = @"";
        protected string _YoutubeLiveAuthURL = @"";
        #endregion

        #region Constructor
        public APIController(IConfiguration configuration)
        {
            _config = configuration;
        }
        #endregion Constructor

        #region Methods
        protected RestRequest CreateRestRequest(AuthToken tkn_details)
        {

            RestRequest req_obj = new RestRequest();

            req_obj.Method = Method.Get;
            req_obj.AddHeader("cache-control", "no-cache");
            req_obj.AddHeader("content-type", "application/x-www-form-urlencoded");
            req_obj.AddHeader("Authorization", (tkn_details.token_type == "bearer" ? "Bearer " : "Basic ") + tkn_details.access_token);
            req_obj.AddHeader("Client-Id", _config["ClientId"]);

            return req_obj;
        }
        #endregion

        #region Properties
        protected AuthToken AuthDetails { get; set; }
        #endregion
    }
}
