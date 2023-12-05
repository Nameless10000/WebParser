using Microsoft.AspNetCore.Mvc;

namespace WebParser.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DefaultController : ControllerBase
    {
        protected JsonResult Json(object value) => new(value);
    }
}
