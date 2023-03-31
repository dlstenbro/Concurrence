using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace ConcurrenceAPI.Models
{
    public class ConcurrenceModel
    {
        public class Data
        {
            public string platform { get; set; }
            public string name { get; set; }
            public string title { get; set; }
            public string image_url { get; set; }
            public string game_name { get; set; }
            public string view_count { get; set; }
            public DateTime dateTime { get; set; }
            public List<string> tags { get; set; }
            public string language { get; set; }
        }

        public string page { get; set; }

    }
}
