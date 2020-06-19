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
    public class CartController : Controller
    {
        [HttpPost]
        public ActionResult AddToCart(CartViewModel cart)
        {
            if(AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44358/");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                        var cartRespone = client.PostAsJsonAsync("api/Cart/CartItem", cart).Result;
                        var prod = client.GetAsync($"api/Products/{cart.ProductId}").Result;
                        if (cartRespone.IsSuccessStatusCode && prod.IsSuccessStatusCode)
                        {
                            var prodEdited = prod.Content.ReadAsAsync<ProductViewModel>().Result;
                            prodEdited.QuantityInStock = prodEdited.QuantityInStock - cart.Quantity;
                           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                            var prodEditedResponse = client.PutAsJsonAsync("api/Products", prodEdited).Result;
                            return RedirectToAction("CartContent");
                        }
                    }
                }
                        return View();
            }
        }

        [HttpGet]
        public ActionResult CartContent()
        {
            using(HttpClient client = new HttpClient())
            {
                if (AuthorizationHelper.GetUserInfo() is null)
                {
                    return RedirectToAction("Login", "Account");
                }
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                var productsResponse = client.GetAsync("api/Products").Result;
                if (cartResponse.IsSuccessStatusCode && productsResponse.IsSuccessStatusCode)
                {
                    var cart = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                    var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    List<ProductViewModel> cartProducts = new List<ProductViewModel>();
                    foreach (var item in cart)
                    {
                        cartProducts.Add(products.Find(i => i.Id == item.ProductId));
                    }
                    ViewBag.cartProducts = cartProducts;
                    return View(cart);
                }
            }
            return RedirectToAction("Index" ,"Home");
        }

        [HttpGet]
        public ActionResult CartCounter()
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Index", "Home");
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                if (cartResponse.IsSuccessStatusCode)
                {
                    var cart = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                    ViewBag.counter = cart.Count();
                    return PartialView();
                }
                return RedirectToAction("Index", "Home");

            }
        }
    }
}