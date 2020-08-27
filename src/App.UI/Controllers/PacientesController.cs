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
using App.Application.Interfaces;

namespace App.UI.Controllers
{
    public class PacientesController : Controller
    {

        //private readonly string URL_API = "playershares.api";
        //private readonly string URL_API = "192.168.99.100:20001";
        private readonly IConfiguration _configuration;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPsicologoRepository _psicologoRepository;



        public PacientesController(IConfiguration configuration,
            IPacienteRepository pacienteRepository,
            IPsicologoRepository psicologoRepository)
        {
            this._configuration = configuration;
            this._pacienteRepository = pacienteRepository;
            this._psicologoRepository = psicologoRepository;
        }

        [Route("Pacientes/Cadastro")]
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


            if (_pacienteRepository.Insert(paciente))
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }


        }

        [Route("Pacientes/Agenda")]
        public async Task<IActionResult> Agenda(string cpf)
        {

            if (!string.IsNullOrEmpty(cpf))
            {

                var paciente = _pacienteRepository.Select(cpf);

                if (paciente != null)
                {
                    return View("Agenda", paciente);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            else
            {
                return RedirectToAction("Error", "Home");
            }


        }


        [Route("Pacientes/Agendar")]
        public async Task<IActionResult> Agendar(string cpf)
        {

            if (!string.IsNullOrEmpty(cpf))
            {

                ViewBag.ListaPsicologo = _psicologoRepository.GetAll().ToList();
                ViewBag.CPF = cpf;

                return View("Agendar");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }



        [Route("Pacientes/SalvarAgenda")]
        public async Task<IActionResult> SalvarAgenda(string cpf
            , string data_consulta
            , string horario_consulta
            , string cboMedico)
        {


            if (!string.IsNullOrEmpty(cpf))
            {

                bool retorno = _pacienteRepository.InsertAgenda(cpf, cboMedico, Convert.ToDateTime(data_consulta), horario_consulta);

                if (retorno)
                {
                    var paciente = _pacienteRepository.Select(cpf);

                    return View("Agenda", paciente);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }

    }
}
