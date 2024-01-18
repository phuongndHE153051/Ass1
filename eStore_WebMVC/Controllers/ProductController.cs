using eStore_WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace eStore_WebMVC.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string? productName, decimal price)
        {
            string productUri = "http://localhost:5220/api/Product";
            string categoryUri = "http://localhost:5220/api/Category";
            List<Product> products = new List<Product>();
            /*List<Category> categories = new List<Category>();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(categoryUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        categories = JsonConvert.DeserializeObject<List<Category>>(data);

                    }
                }
            }*/

            if (!string.IsNullOrEmpty(productName))
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(productUri + "/productName?productName=" + productName))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            products = JsonConvert.DeserializeObject<List<Product>>(data);

                        }
                    }
                }
            }
            else if (price != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(productUri + "/price?price=" + price))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            products = JsonConvert.DeserializeObject<List<Product>>(data);
                            
                        }
                    }
                }
            }
            else
            {
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
            }
            ViewBag.title = productName;
            ViewBag.price = price;
            return View(products);
        }
        public async Task<IActionResult> Add()
        {
            string categoryUri = "http://localhost:5220/api/Category";
            List<Category> categories = new List<Category>();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(categoryUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        categories = JsonConvert.DeserializeObject<List<Category>>(data);

                    }
                }
            }
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            string productUri = "http://localhost:5220/api/Product";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(productUri, product))
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
            Product product = new Product();
            string productUri = "http://localhost:5220/api/Product";
            string categoryUri = "http://localhost:5220/api/Category";
            List<Category> categories = new List<Category>();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(categoryUri))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        categories = JsonConvert.DeserializeObject<List<Category>>(data);

                    }
                }
            }
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(productUri + "/id?id=" + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = content.ReadAsStringAsync().Result;
                        product = JsonConvert.DeserializeObject<Product>(data);

                    }
                }
            }
            ViewBag.categories = categories;
            ViewBag.categoryId = product.CategoryId;
            return View(product);

        }

        [HttpPost]

        public async Task<IActionResult> UpdateProduct(Product product)
        {
            string productUri = "http://localhost:5220/api/Product";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsJsonAsync(productUri + "?id=" + product.ProductId, product))
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
            string productUri = "http://localhost:5220/api/Product";
            string message = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(productUri + "?id=" + id))
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
            List<Product> products = new List<Product>();
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
            return RedirectToAction("Index", products);

        }

    }
}
