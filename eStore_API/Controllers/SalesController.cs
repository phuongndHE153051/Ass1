using eStore_API.Models;
using eStore_API.Modelss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSalesStatistics(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var context = new Assignment01_PRN231Context())
                {
                    var sales = context.OrderDetails.Where(od => od.Order.OrderDate >= startDate && od.Order.OrderDate <= endDate)
                        .GroupBy(od => new
                        {
                            od.Product.ProductId,
                            od.Product.ProductName
                        }
                        )
                        .Select(group => new
                        {
                            ProductId = group.Key.ProductId,
                            ProductName = group.Key.ProductName,
                            TotalQuantity = group.Sum(od => od.Quantity),
                            TotalSales = group.Sum(od => od.UnitPrice * od.Quantity * (1 - od.Discount))
                        })
                        .OrderByDescending(result => result.TotalSales).ToList();
                    return Ok(sales);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
