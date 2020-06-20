using MedicalStore.Helpers;
using MedicalStore.Models;
using MedicalStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public ActionResult OrderInfo()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44358/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                    var orderRespone = client.PostAsJsonAsync("api/Order/AddOrderWithItems",order).Result;
                    if (orderRespone.IsSuccessStatusCode)
                    {
                        return RedirectToAction("PaymentMethod");
                    }
                    return RedirectToAction("Error", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult PaymentMethod()
        {
            return View();
        }

    }
}