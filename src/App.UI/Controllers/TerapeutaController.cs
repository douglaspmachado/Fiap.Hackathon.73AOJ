using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace App.UI.Controllers
{
    public class TerapeutaController : Controller
    {
        //private readonly string URL_API = "playershares.api";
        //private readonly string URL_API = "192.168.99.100:20001";
        private readonly IConfiguration _configuration;
        private readonly ICommonRepository _commonRepository;
        private readonly IPsicologoRepository _psicologoRepository;


        public TerapeutaController(IConfiguration configuration,
                    ICommonRepository commonRepository,
                    IPsicologoRepository psicologoRepository)
        {
            this._configuration = configuration;
            this._commonRepository = commonRepository;
            this._psicologoRepository = psicologoRepository;
        }



        [Route("Terapeuta/Cadastro")]
        public IActionResult Cadastro()
        {
            ViewBag.Abordagem = _commonRepository.GetAbordagens().ToList();
            ViewBag.Atendimento = _commonRepository.GetAtendimento().ToList();
            

            return View("Cadastro");
        }

        [Route("Terapeuta/Pesquisa")]
        public IActionResult Pesquisa()
        {
            ViewBag.Psicologo = _psicologoRepository.GetAll().ToList();    
            return View("Pesquisa");
        }


        [HttpPost]
        public async Task<IActionResult> Salvar(string cpf,
             string nome,
             string sobrenome,
             string email,
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
            string complemento,
            string crp,
            string cboGraduacao,
            string instituicao,
            string curso,
            string anoinicio,
            string anotermino,
            string descricao, int[] CodigoAbordagem, int[] CodigoAtendimento)
        {

            var psicologo = new Psicologo();
            psicologo.CPF_CNPJ = cpf.Replace(".", "").Replace("-", "");
            psicologo.Nome = nome;
            psicologo.Email = email;
            psicologo.Senha = senha;
            psicologo.Sobrenome = sobrenome;
            psicologo.DataNascimento = Convert.ToDateTime(dtnascimento);
            psicologo.Celular = celular;
            psicologo.Endereco = new Endereco();
            psicologo.Endereco.CEP = cep;
            psicologo.Endereco.Bairro = bairro;
            psicologo.Endereco.Cidade = cidade;
            psicologo.Endereco.Complemento = complemento;
            psicologo.Endereco.Estado = estado;
            psicologo.Endereco.Logradouro = logradouro;
            psicologo.Endereco.Numero = numero;
            psicologo.Endereco.Pais = pais;
            psicologo.Perfil = new Perfil();
            psicologo.Perfil.CodigoPerfil = 2; //2- Terapeuta
            psicologo.CRP = crp;
            psicologo.CodGraduacao = cboGraduacao;
            psicologo.InstituicaoEnsino = instituicao;
            psicologo.Curso = curso;
            psicologo.AnoInicio = anoinicio;
            psicologo.AnoTermino = anotermino;
            psicologo.DescricaoAtuacao = descricao;
            psicologo.AreaEstudo = string.Empty;


            if (_psicologoRepository.Insert(psicologo))
            {

                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View("Error");
            }


        }

        [Route("Terapeuta/Agenda")]
        public async Task<IActionResult> Agenda(string cpf)
        {
           

            if (!string.IsNullOrEmpty(cpf))
            {

                var psicologo = _psicologoRepository.Select(cpf);

                if (psicologo != null)
                {
                    return View("Agenda", psicologo);
                }
                else
                {
                    return View("Error");
                }

            }
            else
            {
                return View("Error");
            }

           
        }

    }
}