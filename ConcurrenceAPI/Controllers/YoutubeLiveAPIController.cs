using RestSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ConcurrenceAPI.Interfaces;

namespace ConcurrenceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YoutubeLiveAPIController : APIController, IAPIController
    {
        public YoutubeLiveAPIController(IConfiguration configuration) : base(configuration)
        {

        }

    }
}
