using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace ConcurrenceAPI.Models.Twitch
{
    public class TwitchAPIModel
    {
        public List<stream> data { get; set; }
        public Page pagination { get; set; }
        public class Page 
        {
            public string cursor { get; set; }
        }
        public class stream
        {
            public string id { get; set; }
            public string user_id { get; set; }
            public string user_login { get; set; }
            public string user_name { get; set; }
            public string game_id { get; set; }
            public string game_name { get; set; }
            public string type { get; set; }
            public string title { get; set; }
            public int viewer_count { get; set; }
            public DateTime started_at { get; set; }
            public string language { get; set; }
            public string thumbnail_url { get; set; }
            public List<string> tag_ids { get; set; }
            public bool is_mature { get; set; }
            public string platform { get; set; } = "twitch";
        }
    }
}
