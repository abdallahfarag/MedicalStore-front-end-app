using MedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MedicalStore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            Session["id"] = null;
            Session["username"] = null;
            Session["accessToken"] = null;
            Session["userinfo"] = null;
            return View();
        }
    }
}