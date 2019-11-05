using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Security.Api.Extensions.CustomAuthorize;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        [ClaimsAuthorize("USER", "Atualizar")]
        public ActionResult Get()
        {
            return Ok("Muito bom!!!");
        }
    }
}