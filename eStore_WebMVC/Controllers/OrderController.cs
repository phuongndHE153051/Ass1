using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStore_WebMVC.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Authentication");
            }


            string orderUri = "http://localhost:5220/api/Order";
            if (user != "admin@estore.com")
            {
                orderUri += "?memberEmail=" + user;
            }
            List<Order> orders = new List<Order>();
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
            return View(orders);
        }

        public async Task<IActionResult> Add()
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Authentication");
            }

            string memberUri = "http://localhost:5220/api/Member";
            List<Member> member = new List<Member>();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(memberUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        member = JsonConvert.DeserializeObject<List<Member>>(data);

                    }
                }
            }
            ViewBag.members = member;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto order)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Authentication");
            }
            string orderUri = "http://localhost:5220/api/Order";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(orderUri, order))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        message = "Add success!";
                    }
                    else
                    {
                        string reason = await res.Content.ReadAsStringAsync();
                        message = "Add fail!";
                    }
                    return RedirectToAction("Index");
                }
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Authentication");
            }
            Order order = new Order();
            string orderUri = "http://localhost:5220/api/Order";
            string memberUri = "http://localhost:5220/api/Member";
            List<Member> members = new List<Member>();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(memberUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        members = JsonConvert.DeserializeObject<List<Member>>(data);

                    }
                }
            }
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(orderUri + "/id?id=" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        order = JsonConvert.DeserializeObject<Order>(data);

                    }
                }
            }
            ViewBag.members = members;
            ViewBag.memberId = order.MemberId;
            return View(order);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Authentication");
            }
            string orderUri = "http://localhost:5220/api/Order";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsJsonAsync(orderUri + "?id=" + order.OrderId, order))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        message = "Update success!";
                    }
                    else
                    {
                        message = "Update fail!";
                    }
                    return RedirectToAction("Index");
                }
            }
        }

        public async Task<IActionResult> DeleteAsyn(int id)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Authentication");
            }
            string orderUri = "http://localhost:5220/api/Order";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(orderUri + "?id=" + id))
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
            List<Order> orders = new List<Order>();
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
            return RedirectToAction("Index", orders);

        }

    }


}
