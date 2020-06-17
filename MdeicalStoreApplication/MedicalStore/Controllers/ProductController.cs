using MedicalStore.Models;
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

            return PartialView();
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
            
            return View();
        }
    }
}