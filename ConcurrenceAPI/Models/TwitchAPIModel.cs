using System;
using System.Collections.Generic;

namespace ConcurrenceAPI.Models.Twitch
{
    public class TwitchAPIModel
    {
        private List<TwitchStreamMeta> streams;
        #region Properties
        public List<TwitchStreamMeta> TwitchStreams {
            get
            {
                if (streams == null) streams = new List<TwitchStreamMeta>();
                return streams;
            }
            set
            {
                streams = value;
            } 
        }
        #endregion Properties
    }

    public class StreamDataResponse
    {
        public TwitchAPIModel Data { get; set; }
        public string Page { get; set; }
    }

    public class TwitchStreamMeta
    {
        #region Properties
        public string id { get; set; }              = string.Empty;
        public int user_id { get; set; }            = 0;
        public string user_login { get; set; }      = string.Empty;
        public string user_name { get; set; }       = string.Empty;
        public int? game_id { get; set; }            = 0;
        public string game_name { get; set; }       = string.Empty;
        public string type { get; set; }            = string.Empty;
        public string title { get; set; }           = string.Empty;
        public int viewer_count { get; set; }       = 0;
        public DateTime started_at { get; set; }    = DateTime.UnixEpoch;
        public string language { get; set; }        = string.Empty;
        public string thumbnail_url { get; set; }   = string.Empty;
        public List<string> tag_ids { get; set; }   = new List<string>();
        public bool is_mature { get; set; }         = false;
        public string platform { get; set; }        = "twitch";
        #endregion Properties
    }
}
