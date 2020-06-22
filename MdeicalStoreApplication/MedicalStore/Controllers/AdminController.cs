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

       
    }
}