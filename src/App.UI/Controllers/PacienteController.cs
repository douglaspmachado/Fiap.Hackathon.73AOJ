using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Controllers
{
    public class PacienteController : Controller
    {

        
         [Route("Paciente/Cadastro")]
        public IActionResult Cadastro()
        {
            return View("Cadastro");
        }

    }
}