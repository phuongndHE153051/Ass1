using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eStore_WebMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            Logout();
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            var session = this.HttpContext.Session;
            session.Remove("user");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Authentication(Authentication auth)
        {
            var session = this.HttpContext.Session;
            string authUri = "http://localhost:5220/api/Authentication";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(authUri, auth))
                {
                    using (HttpContent content = res.Content)
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            session.SetString("user", auth.Email);
                            return RedirectToAction("Index", "Home");
                        }
                        else if (res.StatusCode == HttpStatusCode.Unauthorized)
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
