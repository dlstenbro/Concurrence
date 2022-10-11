using ConcurrenceAPI.Common;

using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Microsoft.Extensions.Configuration;

namespace ConcurrenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitchAPIController : ControllerBase
    {
        private readonly IConfiguration _config;

        #region API Endpoints

        private string _APIStreamsURL = @"https://api.twitch.tv/helix/streams";
        private string _APIAuthURL = @"https://id.twitch.tv/oauth2/token";

        #endregion API Endpoints

        #region Constructor
        public TwitchAPIController(IConfiguration configuration)
        {
            _config = configuration;
        }
        #endregion Constructor

        #region Routes
        [HttpGet]
        [Route("/GetAuthToken")]
        public AuthToken GetAuthToken()
        {
            return new OAuthConnector(_APIAuthURL)
                .GetAuthToken("client_credentials",
                    _config["TwitchAPI:ClientId"], 
                    _config["TwitchAPI:ClientSecret"]
                    );
        }

        [HttpGet]
        [Route("/")]
        public object GetStreams()
        {
            if(AuthDetails == null)
            {
                //Redirect("/GetAuthToken");
                AuthDetails = GetAuthToken();
            }

            RestRequest request = new RestRequest();
            RestClient client = new RestClient(_APIStreamsURL);

            request.Method = Method.Get;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", (AuthDetails.token_type == "bearer" ? "Bearer " : "Basic ") + AuthDetails.access_token);
            request.AddHeader("Client-Id", _config["TwitchAPI:ClientId"]);

            RestResponse res = client.Execute(request);

            return res.Content;

        }
        #endregion Routes

        #region Properties
        private AuthToken AuthDetails { get; set; }
        #endregion
    }
}
