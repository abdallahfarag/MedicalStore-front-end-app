using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}