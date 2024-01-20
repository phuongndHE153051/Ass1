using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStoreMVC.Controllers
{
    public class OrderDetailController : Controller
    {
        
        
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            string orderDetailUri = "http://localhost:5220/api/OrderDetails";
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(orderDetailUri + "/id?id=" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(data);
                    }
                }
            }
            ViewBag.orderId = id;
            return View(orderDetails);
        }
        public async Task<IActionResult> Add()
        {
            string orderUri = "http://localhost:5220/api/Order";
            string productUri = "http://localhost:5220/api/Product";

            List<Order> orders = new List<Order>();
            List<Product> products = new List<Product>();

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(orderUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        orders = JsonConvert.DeserializeObject<List<Order>>(data);
                    }
                }
            }

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(productUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        products = JsonConvert.DeserializeObject<List<Product>>(data);
                    }
                }
            }

            ViewBag.orders = orders;
            ViewBag.products = products;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(OrderDetail orderDetail)
        {
            string orderDetailUri = "http://localhost:5220/api/OrderDetails";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(orderDetailUri, orderDetail))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        message = "Add success!";
                    }
                    else
                    {
                        message = "Add fail!";
                    }
                    return RedirectToAction("Index");
                }
            }
        }

        

        

        public async Task<IActionResult> Delete(int id, int orderId)
        {
            string orderDetailUri = "http://localhost:5220/api/OrderDetails";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(orderDetailUri + "?id=" + id))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        message = "Delete success!";
                    }
                    else
                    {
                        message = "Delete fail!";
                    }
                }
            }



            return RedirectToAction("Index", new { id = orderId });
        }
    }
}
