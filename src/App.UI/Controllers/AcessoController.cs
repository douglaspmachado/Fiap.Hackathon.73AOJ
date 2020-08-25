using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace App.UI.Controllers
{
    public class AcessoController : Controller
    {
        //private readonly string URL_API = "playershares.api";
        //private readonly string URL_API = "192.168.99.100:20001";
        private readonly IConfiguration _configuration;
        //public string idJogador;

        public AcessoController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        [Route("Login")]
        public IActionResult Login()
        {

            return View("Cadastro");

        }

    }
}