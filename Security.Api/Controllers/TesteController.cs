using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        public ActionResult Get()
        {
            return Ok("Muito bom!!!");
        }
    }
}