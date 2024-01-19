using eStore_API.Models;
using eStore_API.Modelss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using(var context = new Assignment01_PRN231Context())
            {
                try
                {
                    var data = context.Members.ToList();
                    if(data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }
                catch(Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            using (var context = new Assignment01_PRN231Context())
            {
                try
                {
                    var data = context.Members.FirstOrDefault(m => m.MemberId == id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    return Ok(data);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
        [HttpPost]
        public IActionResult Post(Member m)
        {
            using (var context = new Assignment01_PRN231Context())
            {
                try
                {
                    context.Members.Add(m);
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpPut]
        public IActionResult Put(int id, Member m)
        {
            using (var context = new Assignment01_PRN231Context())
            {
                try
                {
                    var data = context.Members.FirstOrDefault(m=> m.MemberId == id);
                    if(data == null)
                    {
                        return NotFound();
                    }
                    data.Email= m.Email;
                    data.CompanyName= m.CompanyName;
                    data.City= m.City;
                    data.Country= m.Country;
                    data.Password= m.Password;
                    context.Members.Update(data);
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var context = new Assignment01_PRN231Context())
            {
                try
                {
                    var data = context.Members.FirstOrDefault(m => m.MemberId == id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    context.Members.Remove(data);
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }

    }
}
