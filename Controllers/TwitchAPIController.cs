using ConcurrenceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ConcurrenceAPI.Common;

namespace ConcurrenceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TwitchAPIController : ControllerBase
    {
        #region Enviroment Variables

        private string api_client_id = Environment.GetEnvironmentVariable("TWITCH_CLIENT_ID");
        private string api_access_token = Environment.GetEnvironmentVariable("TWITCH_CLIENT_SECRET");
        
        #endregion Enviroment Variables

        #region API Endpoints
        
        private string api_streams_url = @"https://api.twitch.tv/helix/streams";
        
        #endregion API Endpoints

        private OAuthConnector connector;
        private TwitchAPIModel model;

        public TwitchAPIController(TwitchAPIModel model)
        {
            this.model = model;

            connector = new OAuthConnector(api_streams_url);
            connector.GetAuthToken("client_credientials", api_client_id, api_access_token);
        }

        private readonly ILogger<TwitchAPIController> _logger;

        public TwitchAPIController(ILogger<TwitchAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TwitchAPIModel> Get()
        {
            // request all streams and update the model with the new list
            return Enumerable.Range(1, 5).Select(index => new TwitchAPIModel
            {
                //Date = DateTime.Now.AddDays(index),
                //TemperatureC = rng.Next(-20, 55),
                //Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
