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
        private readonly IPsicologoRepository _ipsicologoRepository;

        public TerapeutaController(IConfiguration configuration, ICommonRepository commonRepository, IPsicologoRepository ipsicologoRepository)
        {
            this._configuration = configuration;
            this._commonRepository = commonRepository;
            this._ipsicologoRepository = ipsicologoRepository;
        }

        //public TerapeutaController(ICommonRepository commonRepository)
        //{
        //    this._commonRepository = commonRepository;
        //}

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
            ViewBag.Psicologo = _ipsicologoRepository.GetAll().ToList();    
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
            psicologo.CPF_CNPJ = cpf;
            psicologo.Nome = nome;
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

            //CodigoAbordagem.ToList().ForEach(p =>
            //{
            //    psicologo.Abordagens.ToList().Add(_commonRepository.GetAbordagens().SingleOrDefault(a => a.CodigoAbordagem == p));

            //});

            //CodigoAtendimento.ToList().ForEach(p =>
            //{
            //    psicologo.Atendimentos.ToList().Add(_commonRepository.GetAtendimento().SingleOrDefault(a => a.CodigoAtendimento == p));

            //});



            var json = JsonConvert.SerializeObject(psicologo);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = string.Format("http://{0}/api/Psicologo/Insert", _configuration["ServicenameAPI"]);


            using (var client = new HttpClient())
            {
                var httpResponse = await client.PostAsync(url, data);

                var idPsicologo = httpResponse.Content.ReadAsStringAsync().Result;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.Message = "Error";
                    return View("Error");
                }

            }

        }

        [HttpGet]
        [Route("Terapeuta/Agenda/{cpf}")]
        public async Task<IActionResult> Psicologos(string cpf)
        {

            //Recupera apos o login
            cpf = HttpContext.Session.GetString("cpf");

            if (!string.IsNullOrEmpty(cpf))
            {

                var url = string.Format("http://{0}/api/Psicologos/Select/{0}", _configuration["ServicenameAPI"], cpf);


                using (var client = new HttpClient())
                {
                    var httpResponse = await client.GetAsync(url);

                    var data = httpResponse.Content.ReadAsStringAsync().Result;

                    var psicologo = JsonConvert.DeserializeObject<Psicologo>(data);

                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return View("Agenda", psicologo);
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
}