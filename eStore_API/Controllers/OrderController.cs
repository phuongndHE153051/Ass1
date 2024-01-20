using eStore_API.Models;
using eStore_API.Modelss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string? memberEmail)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Orders.Include(o => o.Member).AsQueryable();
                    if (!string.IsNullOrEmpty(memberEmail))
                    {
                        data = data.Where(o => o.Member.Email == memberEmail);
                    }
                    return Ok(data.ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Orders.Include(o => o.Member).FirstOrDefault(o => o.OrderId == id);
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

        [HttpPost]
        public IActionResult Post(CreateOrderDto o)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    Order order = new Order()
                    {
                        Freight = o.Freight,
                        MemberId = o.MemberId,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate
                    };

                    context.Orders.Add(order);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, CreateOrderDto o)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Orders.FirstOrDefault(o => o.OrderId == id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    data.MemberId = o.MemberId;
                    data.OrderDate = o.OrderDate;
                    data.RequiredDate = o.RequiredDate;
                    data.ShippedDate = o.ShippedDate;
                    data.Freight = o.Freight;

                    context.Orders.Update(data);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Orders.FirstOrDefault(o => o.OrderId == id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    context.Orders.Remove(data);
                    context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
