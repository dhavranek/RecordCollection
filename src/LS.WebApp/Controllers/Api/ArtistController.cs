using System.Web.Http;
using RC.Domain;
using RC.Services;

namespace RC.WebApp.Controllers.Api
{
    public class ArtistController : BaseApiController
    {
        [HttpPost]
        [Route("api/artist/add")]
        public IHttpActionResult Add(string name)
        {
            var service = new ArtistDataService();
            var artist = new Artist();
            artist.Name = name;

            service.Add(artist);
        }
    }
}