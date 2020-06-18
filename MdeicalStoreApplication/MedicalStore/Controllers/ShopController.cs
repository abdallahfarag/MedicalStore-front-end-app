using MedicalStore.Helpers;
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
        public ActionResult ProductDetails(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var productResponse = client.GetAsync($"api/Products/{id}").Result;
                var categoriesResponse = client.GetAsync("api/Categories").Result;
                if (productResponse.IsSuccessStatusCode && categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = categoriesResponse.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    var product = productResponse.Content.ReadAsAsync<ProductViewModel>().Result;
                    var categoryName = categories.SingleOrDefault(item => item.Id == product.CategoryId);
                    ViewBag.catName = categoryName.Name;
                    ViewBag.prod = product;
                    var cart = new CartViewModel();
                    if(AuthorizationHelper.GetUserInfo() != null)
                    {
                        cart.UserId = AuthorizationHelper.GetUserInfo().Id;
                    }
                    cart.ProductId = product.Id;
                    return View(cart);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}