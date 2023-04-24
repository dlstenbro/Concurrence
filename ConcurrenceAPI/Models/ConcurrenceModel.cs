using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace ConcurrenceAPI.Models
{
    public class ConcurrenceModel
    {
        public List<Data> streams { set; get; }
        public class Data
        {
            public string id { get; set; } = string.Empty;
            public string platform { get; set; } = string.Empty;
            public string creator_name { get; set; } = string.Empty;
            public string title { get; set; } = string.Empty;
            public string thumbnail_img { get; set; } = string.Empty;
            public string game_name { get; set; } = string.Empty;
            public int viewers { get; set; } =0;
            public DateTime dateTime { get; set; } = DateTime.UnixEpoch;
            public List<string> tags { get; set; }
            public string language { get; set; } = string.Empty;
        }

        public string twitch_pageination { get; set; }
        public string youtubelive_pageination { get; set; }

    }
}
