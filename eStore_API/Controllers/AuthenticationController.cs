using eStore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Authentication _auth;
        public AuthenticationController(Authentication auth)
        {
            _auth = auth;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Authentication auth)
        {
            if(auth.Email == _auth.Email && auth.Password == _auth.Password)
            {
                return Ok("Đăng nhập thành công!");
            }
            else
            {
                return Unauthorized("Đăng nhập thất bại!");
            }
        }
    }
}
