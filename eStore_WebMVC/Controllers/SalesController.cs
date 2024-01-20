using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStore_WebMVC.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index() { return View(); }



        [HttpGet]
        public async Task<IActionResult> Statistics(DateTime startDate, DateTime endDate)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }

            string salesUri = "http://localhost:5220/api/Sales";
            List<Salescs> sales = new List<Salescs>();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(salesUri + "?startDate=" + startDate + "&endDate=" + endDate))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        sales = JsonConvert.DeserializeObject<List<Salescs>>(data);

                    }
                }
            }
            return View(sales);
        }
    }
}
