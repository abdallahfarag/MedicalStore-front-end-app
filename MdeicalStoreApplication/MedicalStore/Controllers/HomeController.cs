using MedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var productsResponse = client.GetAsync("api/Products").Result;
                var categoryResponse = client.GetAsync("api/Categories").Result;
                if (productsResponse.IsSuccessStatusCode && categoryResponse.IsSuccessStatusCode)
                {
                    var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    var categories = categoryResponse.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    ViewBag.cats = categories;
                    return View(products);
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel contactUsViewModel)
        {
            if (ModelState.IsValid)
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44358/");
                    var contactUsResponse = client.PostAsJsonAsync("api/ContactUs", contactUsViewModel).Result;
                    if (contactUsResponse.IsSuccessStatusCode)
                    {
                        return View("MessageSent");
                    }
                }
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public ActionResult Error()
        {
            Session["id"] = null;
            Session["username"] = null;
            Session["accessToken"] = null;
            Session["userinfo"] = null;
            return View();
        }
    }
}