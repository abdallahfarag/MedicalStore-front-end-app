using MedicalStore.Helpers;
using MedicalStore.Models;
using MedicalStore.Models.Enums;
using MedicalStore.Models.PayPal;
using PayPal.Api;
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
                       // var prod = client.GetAsync($"api/Products/{cart.ProductId}").Result;
                        if (cartRespone.IsSuccessStatusCode/* && prod.IsSuccessStatusCode*/)
                        {
                           // var prodEdited = prod.Content.ReadAsAsync<ProductViewModel>().Result;
                           // prodEdited.QuantityInStock = prodEdited.QuantityInStock - cart.Quantity;
                           // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                           // var prodEditedResponse = client.PutAsJsonAsync("api/Products", prodEdited).Result;
                            return RedirectToAction("Cart");
                        }
                    }
                }
                return RedirectToAction("Error", "Home");

            }
        }

        [HttpGet]
        public ActionResult Cart()
        {
            return View();
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
                    return PartialView(cart);
                }
            }
            return RedirectToAction("Error", "Home");

        }
        [HttpGet]
        public ActionResult EditCart(int id)
        {
            if(AuthorizationHelper.GetUserInfo() is null)
            {
                RedirectToAction("Login", "Home");
            }
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                if (cartResponse.IsSuccessStatusCode)
                {
                    var cart = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                    var carItemEdit = cart.Find(ww => ww.ProductId == id);
                    Session["cartLastQuantity"] = carItemEdit.Quantity;
                    return PartialView(carItemEdit);
                }

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditCart(CartViewModel cart)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                RedirectToAction("Login", "Home");
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                client.Timeout = TimeSpan.FromMinutes(30);
                var carEditResponse = client.PutAsJsonAsync("api/Cart/CartItem", cart).Result;
                if (carEditResponse.IsSuccessStatusCode)
                {
                    var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                    var productsResponse = client.GetAsync("api/Products").Result;
                    if (cartResponse.IsSuccessStatusCode && productsResponse.IsSuccessStatusCode)
                    {
                        var cartContent = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                        var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                        List<ProductViewModel> cartProducts = new List<ProductViewModel>();
                        foreach (var item in cartContent)
                        {
                            cartProducts.Add(products.Find(i => i.Id == item.ProductId));
                        }
                        ViewBag.cartProducts = cartProducts;
                        return PartialView("CartContent", cartContent);
                    }
                }
                    return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet]
        public ActionResult DeleteCart(int id)
        {
            if (AuthorizationHelper.GetUserInfo() is null)
            {
                RedirectToAction("Login", "Home");
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var deleteItemResponse = client.DeleteAsync($"api/Cart/CartItem?userId={AuthorizationHelper.GetUserInfo().Id}&ProductId={id}").Result;
                if (deleteItemResponse.IsSuccessStatusCode)
                {
                    var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                    var productsResponse = client.GetAsync("api/Products").Result;
                    if (cartResponse.IsSuccessStatusCode && productsResponse.IsSuccessStatusCode)
                    {
                        var cartContent = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                        var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                        List<ProductViewModel> cartProducts = new List<ProductViewModel>();
                        foreach (var item in cartContent)
                        {
                            cartProducts.Add(products.Find(i => i.Id == item.ProductId));
                        }
                        ViewBag.cartProducts = cartProducts;
                        return PartialView("CartContent", cartContent);
                    }
                }
                return RedirectToAction("Error", "Home");
            }
        }



        [HttpGet]
        public /*ActionResult*/int CartCounter()
        {
            //if (AuthorizationHelper.GetUserInfo() is null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            using (HttpClient client = new HttpClient())
            {
                    client.BaseAddress = new Uri("https://localhost:44358/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                    var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                    if (cartResponse.IsSuccessStatusCode)
                    {
                        var cart = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                        ViewBag.counter = cart.Count();
                        return /*PartialView()*/cart.Count();
                    }
                return /*RedirectToAction("Error", "Home")*/0;


            }
        }

        //PayPal Payment

        private Payment payment;

        private Payment CreatePayment(APIContext apiContext ,string redirectUrl)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
                var cartResponse = client.GetAsync($"api/Cart/CartUserItem?userId={AuthorizationHelper.GetUserInfo().Id}").Result;
                var productsResponse = client.GetAsync("api/Products").Result;
                //if (cartResponse.IsSuccessStatusCode && productsResponse.IsSuccessStatusCode)
                //{
                    var cart = cartResponse.Content.ReadAsAsync<List<CartViewModel>>().Result;
                    var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    List<ProductViewModel> cartProducts = new List<ProductViewModel>();
                    foreach (var item in cart)
                    {
                        cartProducts.Add(products.Find(i => i.Id == item.ProductId));
                    }

                    decimal totalPrice=0;
                    var listItems = new ItemList(){items = new List<Item>()};
                    foreach (var item in cart)
                    {
                        var product = cartProducts.Find(ww => ww.Id == item.ProductId);

                        listItems.items.Add(new Item() { 
                            name = product.Name,
                            currency = "USD",
                            price = product.Price.ToString(),
                            quantity = item.Quantity.ToString(),
                            sku = "sku"
                        });
                        totalPrice += item.Quantity * product.Price;
                    }

                    var payer = new Payer() { payment_method = "paypal" };
                    var redirUrls = new RedirectUrls() { 
                        cancel_url = redirectUrl,
                        return_url = redirectUrl
                    };
                    var details = new Details()
                    {
                        tax = "1",
                        shipping = "2",
                        subtotal = totalPrice.ToString()
                    };
                    var amount = new Amount()
                    {
                        currency = "USD",
                        total = (Convert.ToDecimal(details.tax) + Convert.ToDecimal(details.shipping) + Convert.ToDecimal(details.subtotal)).ToString(),
                        details = details
                    };
                    var transactionList = new List<Transaction>();
                    transactionList.Add(new Transaction()
                    {
                        description ="Medical store payment",
                        invoice_number = Convert.ToString((new Random()).Next(100000)),
                        amount = amount,
                        item_list = listItems
                    });
                    payment = new Payment()
                    {
                        intent = "sale",
                        payer = payer,
                        transactions = transactionList,
                        redirect_urls = redirUrls
                    };

                    return payment.Create(apiContext);
            }
        }

        private Payment ExecutePayment(APIContext apiContext ,string payerId ,string paymentId)
        {
            var paymentExcution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExcution);
        }

        public ActionResult PaymentWithPaypal()
        {
            if(AuthorizationHelper.GetUserInfo() is null)
            {
                return RedirectToAction("Login", "Account");
            }
            APIContext apiContext = PayPalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURL = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createPayment = CreatePayment(apiContext, baseURL + "guid=" + guid);
                    var links = createPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if(executePayment.state.ToLower() != "approved")
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
                //PaypalLogger.Log("Error: " + ex.Message);
            }

            return RedirectToAction("OrderData", "Cart" ,PaymentMethod.Paypal);
        }
    }
}