using System;
using System.Linq;
using System.Collections.Generic;
using ConcurrenceAPI.Common;

namespace ConcurrenceAPI.Models
{
    public class TwitchAPIModel
    {
        public List<TwitchStreamMeta> streams;

        #region Constructor
        public TwitchAPIModel()
        {

        }
        #endregion Constructor

        #region Methods

        #endregion Methods

    }

    public class TwitchStreamMeta
    {
        #region Constructor
        public TwitchStreamMeta()
        {

        }
        #endregion Constructor

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
