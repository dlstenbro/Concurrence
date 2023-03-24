namespace ConcurrenceAPI.Models.Secrets
{
    public class OAuthResponse
    {
        public string access_token { get; set; } = string.Empty;
        public int expires_in { get; set; } = 0;
        public string token_type { get; set; } = string.Empty;

    }
    public class TwitchAPISecret
    {
        public static string ClientId { get; set; } = string.Empty;
        public static string ClientSecret { get; set; } = string.Empty;
    }
}
