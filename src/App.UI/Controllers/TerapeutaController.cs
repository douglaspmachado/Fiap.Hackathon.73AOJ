using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Controllers
{
    public class TerapeutaController : Controller
    {

        
         [Route("Terapeuta/Cadastro")]
        public IActionResult Cadastro()
        {
            return View("Cadastro");
        }

         [Route("Terapeuta/Pesquisa")]
        public IActionResult Pesquisa()
        {
            return View("Pesquisa");
        }

    }
}