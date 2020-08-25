using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class PacientesController : Controller
    {

        //private readonly string URL_API = "playershares.api";
        //private readonly string URL_API = "192.168.99.100:20001";
        private readonly IConfiguration _configuration;

        public PacientesController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [Route("Paciente/Cadastro")]
        public IActionResult Cadastro()
        {
            return View("Cadastro");
        }


        [HttpPost]
        public async Task<IActionResult> Salvar(string cpf,
                            string nome,
                            string sobrenome,
                            string senha,
                            string dtnascimento,
                            string celular,
                            string pais,
                            string cep,
                            string estado,
                            string cidade,
                            string bairro,
                            string logradouro,
                            string numero,
                            string complemento)
        {

            var paciente = new Usuario();
            paciente.CPF_CNPJ = cpf;
            paciente.Nome = nome;
            paciente.Sobrenome = sobrenome;
            paciente.DataNascimento = Convert.ToDateTime(dtnascimento);
            paciente.Celular = celular;
            paciente.Endereco = new Endereco();
            paciente.Endereco.CEP = cep;
            paciente.Endereco.Bairro = bairro;
            paciente.Endereco.Cidade = cidade;
            paciente.Endereco.Complemento = complemento;
            paciente.Endereco.Estado = estado;
            paciente.Endereco.Logradouro = logradouro;
            paciente.Endereco.Numero = numero;
            paciente.Endereco.Pais = pais;
            paciente.Perfil = new Perfil();
            paciente.Perfil.CodigoPerfil = 1; //1- Paciente



            var json = JsonConvert.SerializeObject(paciente);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = string.Format("http://{0}/api/Paciente/Insert", _configuration["ServicenameAPI"]);


            using (var client = new HttpClient())
            {
                var httpResponse = await client.PostAsync(url, data);

                var idPaciente = httpResponse.Content.ReadAsStringAsync().Result;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Agendar", "Psicologos", new { id = idPaciente });
                }
                else
                {
                    ViewBag.Message = "Error";
                    return View("Error");
                }

            }

        }
    }
}