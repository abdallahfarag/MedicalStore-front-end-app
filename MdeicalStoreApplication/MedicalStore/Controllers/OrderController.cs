﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}