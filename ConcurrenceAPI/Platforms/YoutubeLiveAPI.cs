using ConcurrenceAPI.Interfaces;
using ConcurrenceAPI.Models.Secrets;
using ConcurrenceAPI.Models.YouTube;

using RestSharp;

using System.Collections.Generic;
using System.Text.Json;

namespace ConcurrenceAPI.Platforms
{
    public class YoutubeLiveAPI : IAPIEndpointConnector
    {
        #region YouTube Live Endpoints
        public const string _YoutubeAPIBase = @"https://youtube.googleapis.com/youtube/v3/search";

        private string _api_key;
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

        public object GetAPIResponse(Dictionary<string, string> parameters = null)
        {
            RestRequest req = CreateRestRequest();
            RestClient client = new RestClient(_YoutubeAPIBase);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    req.AddOrUpdateParameter(parameter.Key, parameter.Value);
                }
            }

            req.AddOrUpdateParameter("key", _api_key);
            RestResponse res = client.Execute(req);

            YoutubeLiveModel? model = JsonSerializer.Deserialize<YoutubeLiveModel>(res.Content);

            return model;
        }

        public OAuthResponse GetAuthToken(string auth_url, string client_id, string client_secret)
        {
            throw new System.NotImplementedException();
        }

        public object GetStreams(int first, string after)
        {
            throw new System.NotImplementedException();
        }

        public object StreamSearchResults(string user_login = "", string game_name = "")
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
