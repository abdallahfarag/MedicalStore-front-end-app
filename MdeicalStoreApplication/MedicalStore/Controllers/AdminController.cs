using MedicalStore.Helpers;
using MedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult AdminDashBoard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Products()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var categoriesResponse = client.GetAsync("api/Categories").Result;
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = categoriesResponse.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    ViewBag.cats = new SelectList(categories, "Id", "Name", 1);
                    return PartialView();

                }
                return RedirectToAction("Error", "Home");

            }
        }

        [HttpGet]
        public ActionResult Prods()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var response = client.GetAsync("api/Products").Result;
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    return PartialView(products);
                }
                return RedirectToAction("Error","home");
            }
        }








        [HttpGet]
        public ActionResult Orders()
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var ordersResponse = client.GetAsync("api/Order/GetAllOrders").Result;

                if (ordersResponse.IsSuccessStatusCode)
                {
                    var orders = ordersResponse.Content.ReadAsAsync<List<AdminOrderViewModel>>().Result;

                    return PartialView(orders);

                }
                return RedirectToAction("Error", "Home");

            }
        }
        [HttpGet]
        public ActionResult Cats()
        {
            using(HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var response = client.GetAsync("api/categories").Result;
                if (response.IsSuccessStatusCode)
                {
                    var categories = response.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    return PartialView(categories);
                }
            }
            return RedirectToAction("error", "home");
        }
    }
}