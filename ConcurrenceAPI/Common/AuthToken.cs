using System;

namespace ConcurrenceAPI.Common
{
    public class AuthToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }

    }
    public class TwitchAPISecret
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}

