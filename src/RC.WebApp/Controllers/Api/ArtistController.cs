using System.Web.Http;
using RC.Domain;
using RC.Services;

namespace RC.WebApp.Controllers.Api
{
    public class ArtistController : BaseApiController
    {
        [HttpPost]
        [Route("api/artist/add/{name}")]
        public IHttpActionResult Add(string name)
        {
            var service = new ArtistDataService();
            var artist = new Artist {Name = name};

            var addResult = service.Add(artist);
            if (!addResult.IsSuccessful)
            {
                return Ok(addResult.Error);
            }

            return Ok(artist);
        }
    }
}