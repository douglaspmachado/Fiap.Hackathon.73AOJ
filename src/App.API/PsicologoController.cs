using System;
using System.Threading.Tasks;
using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace App.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsicologoController : ControllerBase
    {
        private readonly IPsicologoRepository _psicologoRepository;

        public PsicologoController(IPsicologoRepository psicologoRepository)
        {
            this._psicologoRepository = psicologoRepository;
        }
        /// <summary>
        /// Retorna um paciente específico cadastrados na plataforma
        /// </summary>
        /// <param name="idPsicologo"></param>
        /// <returns></returns>
        // [HttpGet]
        // [Route("Get/{idPsicologo}")]
        public async Task<IActionResult> Get(int idPsicologo)
        {
            try
            {
                Psicologo psicologo = _psicologoRepository.Get(idPsicologo);

                if (psicologo != null)
                {
                    return Ok(psicologo);
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
        public async Task<IActionResult> Add([FromBody]Psicologo psicologo)
        {
            try
            {
                int execCount = _psicologoRepository.Insert(psicologo);

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
        /// <param name="psicologo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody]Psicologo psicologo)
        {
            try
            {

                int execCount = _psicologoRepository.Update(psicologo); 

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



    }
}