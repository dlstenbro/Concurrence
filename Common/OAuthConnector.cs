using Newtonsoft.Json;
using RestSharp;

namespace ConcurrenceAPI.Common
{
    public class OAuthConnector
    {
        public RestClient client;
        public RestRequest request;

        public OAuthConnector(string auth_url)
        {
            // example https://stackoverflow.com/questions/38494279/how-do-i-get-an-oauth-2-0-authentication-token-in-c-sharp
            this.client = new RestClient(auth_url);
            this.request = new RestRequest();
        }

        public RestResponse GetAuthToken(string grant_type, string client_id, string access_token)
        {
            string parameter_vals = $"grant_type={grant_type}&client_id={client_id}&client_secret={access_token}";

            request.Method = Method.Post;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("application/x-www-form-urlencoded", parameter_vals, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);
            if( response.IsSuccessful)
            {
                //response.Content[""]
            }
            return response;
        }
    }
}
