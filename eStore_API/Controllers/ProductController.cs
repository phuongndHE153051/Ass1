using eStore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Products.Include(p => p.Category).ToList();
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
        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Products.Include(p => p.Category).FirstOrDefault(p=>p.ProductId == id);
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

        [HttpGet("productName")]
        public IActionResult Get(string productName) {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Products.Include(p => p.Category).ToList().FindAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
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
        [HttpGet("price")]
        public IActionResult Get(decimal price) {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var data = context.Products.Include(p => p.Category).ToList().FindAll(p => p.UnitPrice == price);
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
        public IActionResult Post(Product p)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id,Product p)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var product = context.Products.FirstOrDefault(m => m.ProductId == id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    product.ProductName = p.ProductName;
                    product.UnitPrice = p.UnitPrice;
                    product.UnitsInStock = p.UnitsInStock;
                    product.CategoryId = p.CategoryId;
                   
                    context.Products.Update(product);
                    context.SaveChanges();
                }
                return Ok();
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
                    var product = context.Products.FirstOrDefault(m => m.ProductId == id);
                    if (product == null)
                    {
                        return NotFound();
                    }

                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
