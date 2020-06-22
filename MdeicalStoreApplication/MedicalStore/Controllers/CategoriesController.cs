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
    public class CategoriesController : Controller
    {

        public ActionResult AllCategories()
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var categoriesResponse = client.GetAsync("api/Categories").Result;
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = categoriesResponse.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    return PartialView(categories);
                }
                return RedirectToAction("Error", "Home");

            }
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel category)
        {
            if(AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44358/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                    var catResponse = client.PostAsJsonAsync("api/Categories",category).Result;
                    if (catResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("AdminDashBoard", "Admin");
                    }
                }
            }
            return RedirectToAction("Error", "Home");
        }


        [HttpGet]
        public ActionResult CategoriesList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var response = client.GetAsync("api/categories").Result;
                if (response.IsSuccessStatusCode)
                {
                    var cats = response.Content.ReadAsAsync<List<CategoryViewModel>>().Result;
                    return PartialView(cats);
                }
            }
            return RedirectToAction("error", "home");
        }

    }
}