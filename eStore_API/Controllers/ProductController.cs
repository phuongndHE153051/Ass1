using eStore_API.Models;
using eStore_API.Modelss;
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
                    var data = context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
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
        public IActionResult Get(string productName)
        {
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
        public IActionResult Get(decimal price)
        {
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
        public IActionResult Post(CreateProductDto createProductDto)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    Product p = new Product()
                    {
                        CategoryId = createProductDto.CategoryId,
                        ProductId = createProductDto.ProductId,
                        ProductName = createProductDto.ProductName,
                        UnitInStock = createProductDto.UnitInStock,
                        UnitPrice = createProductDto.UnitPrice,
                        Weight = createProductDto.Weight
                    };
                    context.Products.Add(p);
                    context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, CreateProductDto p)
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
                    product.UnitInStock = p.UnitInStock;
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
