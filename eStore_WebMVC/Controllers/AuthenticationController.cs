using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eStore_WebMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(Authentication auth)
        {
            string authUri = "http://localhost:5220/api/Authentication";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(authUri, auth))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Product");
                        }
                        else if(res.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return RedirectToAction("Index", "Authentication");
                        }
                        else
                        {
                            return View("ErrorView");
                        }
                        

                    }
                }
            }
        }
    }
}
