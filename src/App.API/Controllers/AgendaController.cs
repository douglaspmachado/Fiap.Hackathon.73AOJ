using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaController(IAgendaRepository agendaRepository)
        {
            this._agendaRepository = agendaRepository;
        }

        /// <summary>
        /// Retorna um paciente específico cadastrados na plataforma
        /// </summary>
        /// <param name="cpf_cnpjPsicologo"></param>
        /// <param name="dataDia"></param>
        /// <returns></returns>
        // [HttpGet]
        // [Route("Get/{cpf_cnpjPsicologo}")]
        public async Task<IActionResult> GetAgenda(string cpf_cnpjPsicologo)
        {
            try
            {
                Agenda agenda = _agendaRepository.Select(cpf_cnpjPsicologo);

                if (agenda != null)
                {
                    return Ok(agenda);
                }
                else
                {
                    return NotFound("Psicologo sem agenda cadastrada não encontrado");
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
        public async Task<IActionResult> Add([FromBody]Agenda agenda)
        {
            try
            {
                int execCount = _agendaRepository.Insert(agenda);

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
    }
}