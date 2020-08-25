using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly string URL_API = "playershares.api";
        //private readonly string URL_API = "192.168.99.100:20001";
        private readonly IConfiguration _configuration;
        //public string idJogador;

        public HomeController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }


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
        

        [Route("Home/Auth")]
        public async Task<IActionResult> Auth(string cpfcnpj, string senha)
        {

            if (string.IsNullOrEmpty(cpfcnpj) && string.IsNullOrEmpty(senha))
            {
                return RedirectToAction("Error", "Home");
            }

            var dadosAcesso = new { cpf = cpfcnpj, senha = senha };

            var json = JsonConvert.SerializeObject(dadosAcesso);

            var dados = new StringContent(json, Encoding.UTF8, "application/json");

            var url = string.Format("http://{0}/api/Paciente/Autenticar", _configuration["ServicenameAPI"]);


            using (var client = new HttpClient())
            {
                var httpResponse = await client.PostAsync(url, dados);

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("cpf", cpfcnpj);
                    return RedirectToAction("Agenda", "Terapeuta", new { cpf = cpfcnpj });
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }

            }

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