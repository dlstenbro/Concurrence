using System;
using System.Linq;
using System.Collections.Generic;
using ConcurrenceAPI.Common;

namespace ConcurrenceAPI.Models
{
    public class TwitchAPIModel
    {
        #region Properties
        public List<TwitchStreamMeta> streams { get; set; }
        #endregion Properties
    }

    public class TwitchStreamMeta
    {
        #region Properties
        public string id { get; set; }
        public int user_id { get; set; }
        public int user_login { get; set; }
        public string user_name { get; set; }
        public int game_id { get; set; }
        public string game_name { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public int viewer_count { get; set; }
        public DateTime started_at { get; set; }
        public string language { get; set; }
        public string thumbnail_url { get; set; }
        public List<string> tag_ids { get; set; }
        public bool is_mature { get; set; }
        #endregion Properties
    }
}
