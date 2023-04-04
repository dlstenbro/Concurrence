using Newtonsoft.Json.Linq;
using RestSharp;

using System;
using System.Collections.Generic;
using System.Text.Json;

using ConcurrenceAPI.Common;
using ConcurrenceAPI.Interfaces;
using ConcurrenceAPI.Models.Secrets;
using ConcurrenceAPI.Models.Twitch;

namespace ConcurrenceAPI.Platforms
{
    public class TwitchAPI : IAPIEndpointConnector
    {
        #region Twitch Endpoints
        private const string _TwitchStreamsURL = @"https://api.twitch.tv/helix/streams";
        private const string _TwitchGamesURL = @"https://api.twitch.tv/helix/games";
        private const string _TwitchAuthURL = @"https://id.twitch.tv/oauth2/token";
        #endregion

        private string _client_id;
        private string _client_secret;
        private OAuthResponse _oAuth;

        #region Constructor
        public TwitchAPI(string client_id, string client_secret) 
        {
            _client_id = client_id;
            _client_secret = client_secret;
            _oAuth = GetAuthToken(_TwitchAuthURL, _client_id, _client_secret);
        }
        #endregion Constructor

        #region Methods
        public object GetStreams(int first, string after)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (first > 0)
            {
                parameters.Add("first", first.ToString());
            }

            if (!string.IsNullOrEmpty(after))
            {
                parameters.Add("after", after);
            }

            /* Process API requests here
             * 
             * We can build out our API response based on the "first" X number of streams
             * and "after" will be the cursor that points to the next data set
            */
            
            return GetAPIResponse(parameters);
        }

        public object StreamSearchResults(string user_login = "", string game_name = "")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(user_login))
            {
                parameters.Add("user_login", user_login);
            }

            if (!string.IsNullOrEmpty(game_name))
            {
                parameters.Add("game_name", game_name);
            }

            return GetAPIResponse(parameters);
        }
        public OAuthResponse GetAuthToken(string auth_url, string client_id, string client_secret)
        {
            if (string.IsNullOrEmpty(_client_id) ||
                string.IsNullOrEmpty(_client_secret))
                throw new Exception("Secrets are not properly configured!");

            return new OAuthConnector(auth_url).GetAuthToken("client_credentials", _client_id, _client_secret);
        }

        public RestRequest CreateRestRequest(OAuthResponse tkn_details)
        {
            RestRequest req = new RestRequest();
            req.Method = Method.Get;
            req.AddHeader("cache-control", "no-cache");
            req.AddHeader("content-type", "application/x-www-form-urlencoded");
            req.AddHeader("Authorization", (tkn_details.token_type == "bearer" ? "Bearer " : "Basic ") + tkn_details.access_token);
            req.AddHeader("Client-Id", _client_id);

            return req;
        }

        public object GetAPIResponse(Dictionary<string, string> parameters = null)
        {
            RestRequest req = CreateRestRequest(_oAuth);

            if (parameters is not null && parameters.Keys.Count > 0)
            {
                foreach (KeyValuePair<string, string> key in parameters)
                {
                    req.AddOrUpdateParameter(key.Key, key.Value);
                }
            }

            RestClient client = new RestClient(_TwitchStreamsURL);
            RestResponse res = client.Execute(req);

            return JsonSerializer.Deserialize<TwitchAPIModel>(res.Content == null ? "" : res.Content);
        }

        public RestRequest CreateRestRequest()
        {
            return CreateRestRequest(_oAuth);
        }
        #endregion
    }
}
