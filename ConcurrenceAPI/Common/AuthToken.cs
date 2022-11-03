using System;

namespace ConcurrenceAPI.Common
{
    public class AuthToken
    {
        public string access_token { get; set; }    = string.Empty;
        public int expires_in { get; set; }         = 0;
        public string token_type { get; set; }      = string.Empty;

    }
    public class TwitchAPISecret
    {
        public string ClientId { get; set; }        = string.Empty;
        public string ClientSecret { get; set; }    = string.Empty;
    }
}

