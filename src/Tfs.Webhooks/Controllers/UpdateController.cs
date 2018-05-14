using System.Threading.Tasks;
using System.Web.Http;
using TFSHolopBot.Services;

namespace TFSHolopBot.Controllers
{
    [RoutePrefix("api")]
    public class UpdateController:ApiController
    {
        private readonly IRequestHandler _requestHandler;

        public UpdateController(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
        }

        [HttpGet]
        [Route("update")]
        public string Index(string name = "default")
        {
            return "Hello Index "+ name;
        }

        //POST api/update
       [HttpPost]
        public async Task<IHttpActionResult> Post(object update)
        {
            await _requestHandler.Processing(update);
            return Ok();
        }
    }
}
