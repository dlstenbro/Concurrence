using System.Reflection;
using System.Text.Json;
using RestSharp;

namespace ConcurrenceAPI.Common
{
    public class OAuthConnector
    {
        private RestClient _client;
        private RestRequest _request;

        public OAuthConnector(string auth_url)
        {
            // example https://stackoverflow.com/questions/38494279/how-do-i-get-an-oauth-2-0-authentication-token-in-c-sharp
            _client = new RestClient(auth_url);
            _request = new RestRequest();
        }

        public AuthToken GetAuthToken(string grant_type, string client_id, string access_token)
        {
            //https://id.twitch.tv/oauth2/token?client_id={{client_id}}&client_secret={{client_secret}}&grant_type=client_credentials&redirect_uri=http://localhost:3000
            string parameter_vals = $"grant_type={grant_type}&client_id={client_id}&client_secret={access_token}";

            _request.Method = Method.Post;
            _request.AddHeader("cache-control", "no-cache");
            _request.AddHeader("content-type", "application/x-www-form-urlencoded");

            _request.AddParameter("application/x-www-form-urlencoded", parameter_vals, ParameterType.RequestBody);

            RestResponse response = _client.Execute(_request);

            AuthToken? tknData = JsonSerializer.Deserialize<AuthToken>(response.Content);

            return tknData;
        }
    }
}
