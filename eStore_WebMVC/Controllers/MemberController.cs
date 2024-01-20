using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStore_WebMVC.Controllers
{
    public class MemberController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }
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
            return View(members);
        }
        public async Task<IActionResult> Add()
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMember(Member member)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }
            string memberUri = "http://localhost:5220/api/Member";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(memberUri, member))
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

        public async Task<IActionResult> Update(int id)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }
            Member member = new Member();
            string memberUri = "http://localhost:5220/api/Member";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(memberUri + "/id?id=" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        member = JsonConvert.DeserializeObject<Member>(data);

                    }
                }
            }
            return View(member);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateMember(Member member)
        {
            var session = this.HttpContext.Session;
            var user = session.GetString("user");
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }
            string memberUri = "http://localhost:5220/api/Member";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsJsonAsync(memberUri + "?id=" + member.MemberId, member))
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
            if (string.IsNullOrEmpty(user) || user != "admin@estore.com")
            {
                return RedirectToAction("Index", "Authentication");
            }
            string memberUri = "http://localhost:5220/api/Member";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(memberUri + "?id=" + id))
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
            return RedirectToAction("Index", members);

        }
    }


}
