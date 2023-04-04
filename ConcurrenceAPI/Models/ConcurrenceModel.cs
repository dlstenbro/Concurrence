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
            public string id { get; set; }
            public string platform { get; set; }
            public string creator_name { get; set; }
            public string title { get; set; }
            public string thumbnail_img { get; set; }
            public string game_name { get; set; }
            public int viewers { get; set; }
            public DateTime dateTime { get; set; }
            public List<string> tags { get; set; }
            public string language { get; set; }
        }

        public string twitch_pageination { get; set; }
        public string youtubelive_pageination { get; set; }

    }
}
