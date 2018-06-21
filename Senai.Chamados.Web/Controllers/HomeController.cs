﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Home Index Controller";
            ViewBag.Mensagem = "Seja bem vindo! ";
            return View();
            // return Content("<h1>teste<h1>");
        }
    }
}