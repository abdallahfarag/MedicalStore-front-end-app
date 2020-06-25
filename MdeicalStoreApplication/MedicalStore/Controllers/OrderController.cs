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
        public ActionResult OrderData(PaymentMethod paymentMethod)
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.PaymentMethod = paymentMethod;
            return View(orderViewModel);
        }
        
        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            if(order.PaymentMethod != PaymentMethod.Paypal)
            {
                order.PaymentMethod = PaymentMethod.Cash;
            }
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44358/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                    var orderRespone = client.PostAsJsonAsync("api/Order/AddOrderWithItems", order).Result;
                    if (orderRespone.IsSuccessStatusCode)
                    {
                        return View("DoneOrder");
                    }
                    return RedirectToAction("Error", "Home");
                }
           
        }

        [HttpGet]
        public ActionResult DoneOrder()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OrdersContent()
        {
            if(AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var ordersResponse = client.GetAsync($"api/Order/GetUserOrders?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                if (ordersResponse.IsSuccessStatusCode)
                {
                    var userOrders = ordersResponse.Content.ReadAsAsync<List<OrderViewModel>>().Result;
                    
                    return View(userOrders.OrderByDescending(i => i.DateAdded));
                }
                return RedirectToAction("Error", "Index");
            }
            
        }

        [HttpGet]
        public ActionResult OrderDetails(int id)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var productsResponse = client.GetAsync("api/Products").Result;
                var orderItemsResponse = client.GetAsync($"api/OrdersItems/OrderItems?orderId={id}").Result;
                if (orderItemsResponse.IsSuccessStatusCode && productsResponse.IsSuccessStatusCode)
                {
                    var orderProducts = new List<ProductViewModel>();
                    var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    var orderItems = orderItemsResponse.Content.ReadAsAsync<List<OrderItemsViewModel>>().Result;
                    foreach (var item in orderItems)
                    {
                        orderProducts.Add(products.Find(ww => ww.Id == item.ProductId));
                    }
                    ViewBag.orderProds = orderProducts;
                    return View(orderItems);
                }
            }

            return View();
        }
    }
}