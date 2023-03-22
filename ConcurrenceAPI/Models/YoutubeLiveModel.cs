using ConcurrenceAPI.Controllers;
using ConcurrenceAPI.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ConcurrenceAPI.Models
{
    public class YoutubeLiveModel : APIController, IAPIController
    {
        public YoutubeLiveModel(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
