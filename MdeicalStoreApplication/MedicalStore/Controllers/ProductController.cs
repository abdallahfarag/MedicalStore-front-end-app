using MedicalStore.Helpers;
using MedicalStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Search()
        {
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                var productsResponse = client.GetAsync("api/Products").Result;

                if (productsResponse.IsSuccessStatusCode)
                {
                    var products = productsResponse.Content.ReadAsAsync<List<ProductViewModel>>().Result;
                    var searchListProductModel = new List<SearchProductViewModel>();

                    foreach (var item in products)
                    {
                        var searchProductModel = new SearchProductViewModel();
                        searchProductModel.Id = item.Id;
                        searchProductModel.Name = item.Name;

                        searchListProductModel.Add(searchProductModel);
                    }

                    return PartialView(searchListProductModel);
                }
                else
                {
                    return PartialView();
                }
            }
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel productViewModel, HttpPostedFileBase prodImg)
        {
           
                #region Convert image to string
                //string theFileName = Path.GetFileName(prodImg.FileName);
                byte[] thePictureAsBytes = new byte[prodImg.ContentLength];
                using (BinaryReader theReader = new BinaryReader(prodImg.InputStream))
                {
                    thePictureAsBytes = theReader.ReadBytes(prodImg.ContentLength);
                }
                string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                #endregion


                productViewModel.Image = thePictureDataAsString;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());

                var response = client.PostAsJsonAsync("api/Products", productViewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("AdminDashBoard", "Admin");
                }
            }

            return RedirectToAction("Error", "Home");

        }

        [HttpDelete]
        public ActionResult DeleteProduct(ProductViewModel product)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());
               
                    var response = client.DeleteAsync($"api/products/{product.Id}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("AdminDashBoard", "Admin");
                    }
                
            }
            return RedirectToAction("Error","home");
        }
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");

                var response = client.GetAsync($"api/products/{id}").Result;

                if (response.IsSuccessStatusCode )
                {
                    var prod = response.Content.ReadAsAsync<ProductViewModel>().Result;

                    return PartialView( prod);
                }
            }
            return RedirectToAction("Error","home");
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductViewModel product)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44358/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["accessToken"].ToString());

                var updatedProduct = client.PutAsJsonAsync("api/Products", product).Result;
                if (updatedProduct.IsSuccessStatusCode)
                {
                    return RedirectToAction("AdminDashBoard", "Admin");

                }              
            }
            return RedirectToAction("error", "home");
        }

    }
}