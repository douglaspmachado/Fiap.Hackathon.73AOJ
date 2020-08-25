using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IPacienteRepository pacienteRepository)
        {
            this._pacienteRepository = pacienteRepository;
        }
        /// <summary>
        /// Retorna um paciente específico cadastrados na plataforma
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        // [HttpGet]
        // [Route("Get/{cpf}")]
        public async Task<IActionResult> Select([FromBody]string cpf)
        {
            try
            {
                Usuario usuario = _pacienteRepository.Select(cpf);

                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("Usuário não encontrado");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Insere um paciente na plataforma
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de Request:
        /// 
        /// POST 
        /// 
        ///     {
        ///         
        ///     
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Add([FromBody]Usuario usuario)
        {
            try
            {
                int execCount = _pacienteRepository.Insert(usuario);

                if (execCount > 0)
                {
                    return Ok(execCount.ToString());
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Atualiza dados do paciente
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de Request:
        /// 
        /// PUT 
        /// 
        ///     {
        ///    
        ///     }
        /// 
        /// 
        /// </remarks>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody]Usuario usuario)
        {
            try
            {

                int execCount = _pacienteRepository.Update(usuario); 

                if (execCount > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }


        [HttpGet]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar(string cpf, string senha)
        {
            

            try
            {
                bool aut = _pacienteRepository.Autenticar(cpf, senha);

                if (aut)
                {
                   return Ok();
                }
                else
                {
                    return NotFound();
                }

                
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}