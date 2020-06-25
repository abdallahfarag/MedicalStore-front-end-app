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

        [HttpDelete]
        public ActionResult DeleteCategory(CategoryViewModel category)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("login", "account");
            }
            using (HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());

                var response = client.DeleteAsync($"api/categories/{category.Id}").Result;
                if (response.IsSuccessStatusCode)
                {
                   return RedirectToAction("AdminDashBoard", "Admin");
                }
            }
            return RedirectToAction("Error", "home");
        }
      
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("login", "account");
            }
            using (HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var response = client.GetAsync($"api/categories/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var category = response.Content.ReadAsAsync<CategoryViewModel>().Result;
                    return PartialView(category);
                }
            }
            return RedirectToAction("error", "home");
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryViewModel category)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("login", "account");
            }
            using (HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());

                var updatedCategory = client.PutAsJsonAsync("api/categories", category).Result;
                if (updatedCategory.IsSuccessStatusCode)
                {
                    return RedirectToAction("AdminDashBoard", "Admin");
                }
            }
            return RedirectToAction("error", "home");
        }

        [HttpGet]
        public ActionResult CategoriesList()
        {
            using (HttpClient client = new HttpClient())
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
    }
}