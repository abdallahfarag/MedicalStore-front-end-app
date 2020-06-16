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
        public ActionResult AddProduct()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel product, HttpPostedFileBase ImageFile)
        {
            try
            {

                #region Data Validation
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {

                    int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".jpeg" };
                    var ext = ImageFile.FileName.Substring(ImageFile.FileName.LastIndexOf('.'));
                    var fileName = ImageFile.FileName.Substring(0, ImageFile.FileName.IndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = $"Please Upload image of type .jpg,.gif,.png,.jpeg.";

                        return View("create");

                    }
                    else if (ImageFile.ContentLength > MaxContentLength)
                    {

                        var message = $"Please Upload a file upto 1 mb.";

                        return View();
                    }
                }
                else
                {
                    return View();
                }


                #endregion

                #region Product Name Unique Validation
                using (var client = new HttpClient())
                {
                    var requestUri2 = $"https://localhost:44358/api/Products/UniqueName/{product.Name}";
                    var result2 = client.PostAsJsonAsync(requestUri2, product.Name).Result;
                    if (!result2.IsSuccessStatusCode)
                    {
                        return View();
                    }

                }
                #endregion

                #region Post Product
                using (var client = new HttpClient())
                {
                    var requestUri2 = "https://localhost:44358/api/Products";
                    var result2 = client.PostAsJsonAsync(requestUri2, product).Result;
                    if (!result2.IsSuccessStatusCode)
                    {
                        return View();
                    }
                }

                #endregion

                #region image

                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        byte[] Bytes = new byte[ImageFile.InputStream.Length + 1];
                        ImageFile.InputStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = ImageFile.FileName };
                        content.Add(fileContent);
                        var requestUri = $"https://localhost:44358/api/Products/UploadImage/{product.Name}";
                        var result = client.PostAsync(requestUri, content).Result;
                        if (!result.IsSuccessStatusCode)
                        {
                            return View();
                        }
                    }


                }
                #endregion

            }
            catch
            {
                return View();
            }
            return View();
        }



    }
}