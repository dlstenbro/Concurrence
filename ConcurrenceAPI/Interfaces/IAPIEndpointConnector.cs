using ConcurrenceAPI.Common;
using ConcurrenceAPI.Models.Secrets;
using ConcurrenceAPI.Models.Twitch;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace ConcurrenceAPI.Interfaces
{
    public interface IAPIEndpointConnector
    {
        #region Methods
        public abstract RestRequest CreateRestRequest();
        public JsonDocument GetAPIResponse(string url, Dictionary<string, string> parameters = null);
        #endregion
    }
}