using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {

        [Authorize(Roles = "superadmin")]
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in HttpContext.User.Claims select new { c.Type, c.Value });
        }

        [Authorize(Roles = "admin")]
        [Route("{id}")]
        [HttpGet]
        public string Get(int id)
        {
            return id.ToString();
        }

    }
}