using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Domain.Entity
{
   public class Psicologo
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CPF_CNPJ { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CRP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CodGraduacao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string InstituicaoEnsino { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Curso { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AnoInicio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AnoTermino { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AreaEstudo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string DescricaoAtuacao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Abordagens Abordagens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Atendimento Atendimento { get; set; }



    }
}
