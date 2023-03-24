using ConcurrenceAPI.Common;
using ConcurrenceAPI.Models.Secrets;
using ConcurrenceAPI.Models.Twitch;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ConcurrenceAPI.Interfaces
{
    public interface IAPIEndpointConnector
    {
        #region Methods
        public abstract object GetStreams(int first, string after);
        public abstract OAuthResponse GetAuthToken(string auth_url, string client_id, string client_secret);
        public abstract object StreamSearchResults(string user_login = "", string game_name = "");
        public abstract RestRequest CreateRestRequest(OAuthResponse tkn_details);
        public abstract StreamDataResponse GetAPIResponse(string url, Dictionary<string, string> parameters = null);
        #endregion
    }
}
