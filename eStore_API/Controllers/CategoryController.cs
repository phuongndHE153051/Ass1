using eStore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Categories.ToList();
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
