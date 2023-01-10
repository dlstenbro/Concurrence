using ConcurrenceAPI.Common;
using ConcurrenceAPI.Models.Twitch;

using RestSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;

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
            if (string.IsNullOrEmpty(_config["ClientId"]) ||
                string.IsNullOrEmpty(_config["ClientSecret"]))
                throw new Exception("Secrets are not properly configured!");

            return new OAuthConnector(_APIAuthURL)
                .GetAuthToken("client_credentials",
                    _config["ClientId"], 
                    _config["ClientSecret"]
                );
        }

        [HttpGet]
        [Route("/")]
        public object GetStreams(int first, string after)
        {
            if(AuthDetails == null)
            {
                AuthDetails = GetAuthToken();
            }

            RestRequest request = new RestRequest();
            RestClient client = new RestClient(_APIStreamsURL);
            TwitchAPIModel model = new TwitchAPIModel();
            StreamDataResponse response = new StreamDataResponse();

            request.Method = Method.Get;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", (AuthDetails.token_type == "bearer" ? "Bearer " : "Basic ") + AuthDetails.access_token);
            request.AddHeader("Client-Id", _config["ClientId"]);

            if (first > 0)
            {
                request.AddOrUpdateParameter("first", first);
            }

            if (!string.IsNullOrEmpty(after))
            {
                request.AddOrUpdateParameter("after", after);
            }

            /* Process API requests here
             * 
             * We can build out our API response based on the "first" X number of streams
             * and "after" will be the cursor that points to the next data set
            */ 
            RestResponse res = client.Execute(request);

            if (res.IsSuccessful)
            {
                JToken json = JObject.Parse(res.Content == null ? "" : res.Content);

                if (json.HasValues)
                {
                    JToken data = json["data"];
                    foreach (JToken record in data)
                    {
                        TwitchStreamMeta? meta = record.ToObject<TwitchStreamMeta>();
                        model.TwitchStreams.Add(meta);
                    }

                    JToken page = json["pagination"]?["cursor"];

                    response.Data = model;
                    response.Page = page?.Value<string>();
                }
            }

            return response;
        }
        #endregion Routes

        #region Methods
        #endregion

        #region Properties
        private AuthToken AuthDetails { get; set; }
        #endregion
    }
}
