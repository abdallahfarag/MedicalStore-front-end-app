using MedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        public ActionResult Shop()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var productsResponse = client.GetAsync("api/Products").Result;
                var categoriesResponse = client.GetAsync("api/Categories").Result;

                if (productsResponse.IsSuccessStatusCode && categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = categoriesResponse.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    ViewBag.cats = categories;
                    return View(products);
                }
            }
            return RedirectToAction("Index" ,"Home");
        }


        [HttpGet]
        public ActionResult ProductDetails()
        {
            return View();
        }
    }
}