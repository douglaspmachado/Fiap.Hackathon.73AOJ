using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class PsicologosController : Controller
    {

        //private readonly string URL_API = "playershares.api";
        //private readonly string URL_API = "192.168.99.100:20001";
        private readonly IConfiguration _configuration;

        public PsicologosController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [Route("Psicologos/{id}")]
        public async Task<IActionResult> Psicologos(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {

                var url = string.Format("http://{0}/api/Psicologos/Get", _configuration["ServicenameAPI"], id);


                using (var client = new HttpClient())
                {
                    var httpResponse = await client.GetAsync(url);


                    var data = httpResponse.Content.ReadAsStringAsync().Result;

                    var listaPsicologos = JsonConvert.DeserializeObject<IEnumerable<Psicologo>>(data);

                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //return View("Psicologos", listaPsicologos);
                        return View(listaPsicologos);
                    }
                    else
                    {
                        ViewBag.Message = "Error";
                        return View("Error");
                    }

                }

            }

            return View("Psicologos");
        }

    }

    //[Route("Psicologos/Agendar/{id}")]
    //public async Task<IActionResult> Agendar(string idPaciente, string idPsicologo)
    //{

    //    if (!string.IsNullOrEmpty(idPaciente) && !string.IsNullOrEmpty(idPsicologo))
    //    {

    //        var url = string.Format("http://{0}/api/Jogador/Select/{1}", _configuration["ServicenameAPI"], id);


    //        using (var client = new HttpClient())
    //        {
    //            var httpResponse = await client.GetAsync(url);


    //            var data = httpResponse.Content.ReadAsStringAsync().Result;

    //            var jogador = JsonConvert.DeserializeObject<Jogador>(data);

    //            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
    //            {
    //                return View("Detalhe", jogador);
    //            }
    //            else
    //            {
    //                ViewBag.Message = "Error";
    //                return View("Error");
    //            }

    //        }

    //    }

    //    return View("Detalhe");
    //}



}