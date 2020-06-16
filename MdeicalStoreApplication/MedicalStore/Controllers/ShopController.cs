using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        public ActionResult Shop()
        {
            return View();
        }
    }
}