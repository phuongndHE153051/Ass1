using eStore_API.Models;
using eStore_API.Modelss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly Assignment01_PRN231Context assignment01_PRN231Context;
        readonly Authentication authentication;
        public AuthenticationController(Assignment01_PRN231Context assignment01_PRN231Context,
        IOptions<Authentication> options)
        {
            this.assignment01_PRN231Context = assignment01_PRN231Context;
            this.authentication = options.Value;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Authentication auth)
        {
            if (
                (auth.Email == auth.Email && auth.Password == auth.Password)
            || assignment01_PRN231Context.Members.Any(m => m.Email == auth.Email && m.Password == auth.Password)
            )
            {
                return Ok("Đăng nhập thành công!");
            }
            return Unauthorized("Đăng nhập thất bại!");

        }
    }
}
