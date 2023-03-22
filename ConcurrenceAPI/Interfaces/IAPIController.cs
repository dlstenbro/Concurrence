using ConcurrenceAPI.Common;
using ConcurrenceAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace ConcurrenceAPI.Interfaces
{
    public interface IAPIController
    {

        [HttpGet]
        [Route("/GetAuthToken")]
        public static void GetAuthToken() {}

        [HttpGet]
        [Route("/")]
        public static void GetStreams() { }

        [HttpGet]
        [Route("/StreamSearch")]
        public static void GetResultsFromSearch() { }
    }
}
