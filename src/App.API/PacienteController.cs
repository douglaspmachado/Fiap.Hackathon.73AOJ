using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace App.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        /// <summary>
        /// Retorna um paciente específico cadastrados na plataforma
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        // [HttpGet]
        // [Route("Get")]
        // public async Task<IActionResult> Get(string cpf)
        // {
        //     try
        //     {
                
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(500);
        //     }

        // }

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
        // [HttpPost]
        // [Route("Insert")]
        // public async Task<IActionResult> Add([FromBody]Paciente paciente)
        // {
        //     try
        //     {

        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(500);
        //     }

        // }

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
        /// <param name="paciente"></param>
        /// <returns></returns>
        // [HttpPut]
        // [Route("Update")]
        // public async Task<IActionResult> Update([FromBody]Paciente paciente)
        // {
        //     try
        //     {

        //         //int execCount = 

        //         if (execCount > 0)
        //         {
        //             return Ok();
        //         }
        //         else
        //         {
        //             return BadRequest();
        //         }

        //     }
        //     catch (Exception)
        //     {

        //         return StatusCode(500);
        //     }
        // }

        /// <summary>
        /// Exclui Jogador
        /// </summary>
        /// <remarks>
        /// 
        /// Exemplo de Request:
        /// 
        /// DELETE 
        /// 
        ///     {
        ///         "cpf": 12345678909,
        ///     }
        /// 
        /// 
        /// </remarks>
        /// <param name="paciente"></param>
        /// <returns></returns>
        // [HttpDelete]
        // [Route("Delete/{cpf}")]
        // public IActionResult Delete(string cpf)
        // {

        //     try
        //     {
        //         //int execCount = 

        //         // if (execCount > 0)
        //         // {
        //         //     return Ok();
        //         // }
        //         // else
        //         // {
        //         //     return NotFound();
        //         // }

        //     }
        //     catch (Exception ex)
        //     {

        //         return StatusCode(500);
        //     }

        // }

    }
}