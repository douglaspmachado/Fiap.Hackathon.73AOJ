using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Controllers
{
    public class HomeController : Controller
    {

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("Home/Error")]
        public IActionResult Error()
        {
            return View("Error");
        }

        [Route("Home/Login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Route("Home/Sobre")]
        public IActionResult Sobre()
        {
            return View("Sobre");
        }

         [Route("Home/Servicos")]
        public IActionResult Servicos()
        {
            return View("Servicos");
        }

    }
}