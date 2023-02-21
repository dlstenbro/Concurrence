using ConcurrenceAPI.Common;
using ConcurrenceAPI.Models.Twitch;

using RestSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

namespace ConcurrenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitchAPIController : ControllerBase
    {
        private readonly IConfiguration _config;

        #region API Endpoints

        private string _APIStreamsURL = @"https://api.twitch.tv/helix/streams";
        private string _APIGamesURL = @"https://api.twitch.tv/helix/games";
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
            RestRequest request = CreateRestRequest();
            RestClient client = new RestClient(_APIStreamsURL);

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
            StreamDataResponse response = GetAPIResponse(_APIStreamsURL);

            return response;
        }

        [HttpGet]
        [Route("/StreamSearch")]
        public object GetResultsFromSearch(string user_login = "", string game_name = "")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if(!string.IsNullOrEmpty(user_login))
            {
                parameters.Add("user_login", user_login);
            }

            if(!string.IsNullOrEmpty(game_name))
            {
                parameters.Add("game_name", game_name);
            }

            StreamDataResponse response = GetAPIResponse(_APIStreamsURL, parameters);

            return response;
        }
        #endregion Routes

        #region Methods

        private StreamDataResponse GetAPIResponse(string url, Dictionary<string, string> parameters = null)
        {
            TwitchAPIModel model = new TwitchAPIModel();

            RestRequest request = CreateRestRequest();
            RestClient client = new RestClient(url);

            if(parameters is not null && parameters.Keys.Count > 0)
            {
                foreach(KeyValuePair<string, string> key in parameters) {
                    request.AddOrUpdateParameter(key.Key, key.Value);
                }
            }

            RestResponse res = client.Execute(request);

            StreamDataResponse response = new StreamDataResponse();

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

        private RestRequest CreateRestRequest()
        {
            if (AuthDetails == null)
            {
                AuthDetails = GetAuthToken();
            }

            RestRequest req_obj = new RestRequest();

            req_obj.Method = Method.Get;
            req_obj.AddHeader("cache-control", "no-cache");
            req_obj.AddHeader("content-type", "application/x-www-form-urlencoded");
            req_obj.AddHeader("Authorization", (AuthDetails.token_type == "bearer" ? "Bearer " : "Basic ") + AuthDetails.access_token);
            req_obj.AddHeader("Client-Id", _config["ClientId"]);

            return req_obj;
        }
        #endregion

        #region Properties
        private AuthToken AuthDetails { get; set; }
        #endregion
    }
}
